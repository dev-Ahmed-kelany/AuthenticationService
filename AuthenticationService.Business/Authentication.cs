using AuthenticationService.Repository;
using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Business.Validation;
using AuthenticationService.Business.Security;
using AuthenticationService.Business.Utilities;
using AuthenticationService.Dtos.AuditLogs;
using AuthenticationService.Dtos.LoginHistory;

namespace AuthenticationService.Business
{
    public static class AuthenticationErrors
    {
        public static readonly Error InvalidCredentials = new Error("User.InvalidCredentials", "Invalid Credentials.", HttpStatus.Unauthorized);
        public static readonly Error InactiveAccount = new Error("User.InactiveAccount", "Account is inactive.", HttpStatus.Unauthorized);
        public static readonly Error InvalidRefreshToken = new Error("Token.InvalidRefreshToken", "Refresh token is invalid.", HttpStatus.Unauthorized);

    }

    public class Authentication
    {
        private static bool PasswordMatches(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        private static bool IsAccountActive(int statusID)
        {
            return statusID == 1;
        }

        private static async Task<Result<AuthenticationUserDto>> AuthenticateUserAsync(AuthenticationRequestDto request)
        {
            var validationResult = AuthenticationValidator.ValidateAuthenticate(request);
            if (!validationResult.IsSuccess) return new Result<AuthenticationUserDto>(validationResult);

            AuthenticationUserDto? user = await AuthenticationRepository.GetAuthenticationUserByUsernameAsync(request.Username);
            if (user == null) return Result<AuthenticationUserDto>.Failure(AuthenticationErrors.InvalidCredentials);

            if (!PasswordMatches(request.Password, user.PasswordHash)) return Result<AuthenticationUserDto>.Failure(AuthenticationErrors.InvalidCredentials);

            if (!IsAccountActive(user.StatusID)) return Result<AuthenticationUserDto>.Failure(AuthenticationErrors.InactiveAccount);

            return Result<AuthenticationUserDto>.Success(user);
        }

        public static async Task<Result<LoginResponseDto>> LoginAsync(AuthenticationRequestDto request) 
        {
            var authenticationResult = await AuthenticateUserAsync(request);
            if (!authenticationResult.IsSuccess)
            {
                var failureLoginHistory = new CreateLoginHistoryDto
                {
                    UserID = authenticationResult.Data.ID,
                    Status = 0,
                    FailureReason = authenticationResult.Error.Description.ToString()
                };
                await LoginHistory.AddAsync(failureLoginHistory);

                return new Result<LoginResponseDto>(authenticationResult);
            }

            var accessTokenRequest = new TokenRequestDto
            {
                UserId = authenticationResult.Data.ID,
                Username = authenticationResult.Data.Username,
                RoleName = authenticationResult.Data.RoleName,
                PermissionsMask = authenticationResult.Data.PermissionsMask
            };

            var accessToken = TokenService.GenerateAccessToken(accessTokenRequest);

            string refreshToken = TokenService.GenerateRefreshToken();
            string refreshTokenHash = Utilities.Utilities.HashRefreshToken(refreshToken);
            DateTime refreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

            await RefreshToken.CreateAsync(refreshTokenHash, authenticationResult.Data.ID, refreshTokenExpiresAt);

            var loginResponse = new LoginResponseDto
            {
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessToken.ExpiresAt
            };

            var successLoginHistory = new CreateLoginHistoryDto
            {
                UserID = authenticationResult.Data.ID,
                Status = 1
            };
            await LoginHistory.AddAsync(successLoginHistory);

            var auditLog = new CreateAuditLogDto
            {
                UserID = authenticationResult.Data.ID,
                EntityID = 1,
                OperationTypeID = 5
            };
            await AuditLog.AddAsync(auditLog);

            return Result<LoginResponseDto>.Success(loginResponse);
        }

        public static async Task<Result> VerifyCredentialsAsync(AuthenticationRequestDto request) { return await AuthenticateUserAsync(request); }

        public static async Task<Result> ChangePasswordAsync(ChangePasswordDto request)
        {
            var authenticationResult = await AuthenticateUserAsync(new AuthenticationRequestDto { Username = request.Username, Password = request.CurrentPassword});
            if (!authenticationResult.IsSuccess) return authenticationResult;

            request.NewPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            
            bool isChanged = await AuthenticationRepository.ChangePasswordAsync(authenticationResult.Data.ID, request.NewPassword);
            if (!isChanged) Result.Failure(AuthenticationErrors.InvalidCredentials);

            return Result.Success();
        }

        public static async Task<Result<LoginResponseDto>> RefreshAsync(RefreshTokenRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return Result<LoginResponseDto>.Failure(AuthenticationErrors.InvalidRefreshToken);

            string refreshTokenHash = Utilities.Utilities.HashRefreshToken(request.RefreshToken);

            RefreshTokenDto? refreshToken = await RefreshToken.GetByHashAsync(refreshTokenHash);

            var validateRefreshToken = AuthenticationValidator.ValidateRefreshToken(refreshToken);
            if (!validateRefreshToken.IsSuccess)
                return new Result<LoginResponseDto>(validateRefreshToken);

            AuthenticatedUserDto? user = await AuthenticationRepository.GetAuthenticatedUserByIDAsync(refreshToken.UserID);

            if (user == null)
                return Result<LoginResponseDto>.Failure(AuthenticationErrors.InvalidRefreshToken);

            if (!IsAccountActive(user.StatusID))
                return Result<LoginResponseDto>.Failure(AuthenticationErrors.InactiveAccount);

            var accessTokenRequest = new TokenRequestDto
            {
                UserId = user.ID,
                Username = user.Username,
                RoleName = user.RoleName,
                PermissionsMask = user.PermissionsMask
            };

            var accessToken = TokenService.GenerateAccessToken(accessTokenRequest);

            await RefreshToken.RevokeAsync(refreshToken.ID);

            string newRefreshToken = TokenService.GenerateRefreshToken();
            string newRefreshTokenHash = Utilities.Utilities.HashRefreshToken(newRefreshToken);
            DateTime newRefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

            await RefreshToken.CreateAsync(newRefreshTokenHash, user.ID, newRefreshTokenExpiresAt);

            var response = new LoginResponseDto
            {
                AccessToken = accessToken.Token,
                RefreshToken = newRefreshToken,
                AccessTokenExpiresAt = newRefreshTokenExpiresAt
            };

            return Result<LoginResponseDto>.Success(response);
        }

        public static async Task<Result<bool>> LogoutAsync(RefreshTokenRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return Result<bool>.Failure(AuthenticationErrors.InvalidRefreshToken);

            string refreshTokenHash = Utilities.Utilities.HashRefreshToken(request.RefreshToken);

            RefreshTokenDto? refreshToken = await RefreshToken.GetByHashAsync(refreshTokenHash);

            var validateRefreshToken = AuthenticationValidator.ValidateRefreshToken(refreshToken);
            if (!validateRefreshToken.IsSuccess)
                return new Result<bool>(validateRefreshToken);

            bool revoked = await RefreshToken.RevokeAsync(refreshToken.ID);

            if (!revoked)
                return Result<bool>.Failure(AuthenticationErrors.InvalidRefreshToken);

            return Result<bool>.Success(true);
        }
    }
}
