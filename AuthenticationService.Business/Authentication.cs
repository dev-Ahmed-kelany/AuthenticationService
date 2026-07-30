using AuthenticationService.Repository;
using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Business.Validation;

namespace AuthenticationService.Business
{
    public static class AuthenticationErrors
    {
        public static readonly Error InvalidCredentials = new Error("User.InvalidCredentials", "Invalid Credentials.", HttpStatus.BadRequest);
        public static readonly Error InactiveAccount = new Error("User.InactiveAccount", "Account is inactive.", HttpStatus.BadRequest);

    }

    public class Authentication
    {
        private static bool PasswordMatches(string password, string passwordHash)
        {
            return password == passwordHash;
        }

        private static bool IsAccountActive(int statusID)
        {
            return statusID == 1;
        }

        private static Result<AuthenticationUserDto> AuthenticateUser(AuthenticationRequestDto request)
        {
            var validationResult = AuthenticationValidator.ValidateAuthenticate(request);
            if (!validationResult.IsSuccess) return new Result<AuthenticationUserDto>(validationResult);

            AuthenticationUserDto? user = AuthenticationRepository.GetAuthenticationUserByUsername(request.Username);
            if (user == null) return Result<AuthenticationUserDto>.Failure(AuthenticationErrors.InvalidCredentials);

            if (!PasswordMatches(request.Password, user.PasswordHash)) return Result<AuthenticationUserDto>.Failure(AuthenticationErrors.InvalidCredentials);

            if (!IsAccountActive(user.StatusID)) return Result<AuthenticationUserDto>.Failure(AuthenticationErrors.InactiveAccount);

            return Result<AuthenticationUserDto>.Success(user);
        }

        public static Result Login(AuthenticationRequestDto request) { return AuthenticateUser(request); }

        public static Result VerifyCredentials(AuthenticationRequestDto request) { return AuthenticateUser(request); }

        public static Result ChangePassword(ChangePasswordDto request)
        {
            var authenticationResult = AuthenticateUser(new AuthenticationRequestDto { Username = request.Username, Password = request.CurrentPassword});
            if (!authenticationResult.IsSuccess) return authenticationResult;

            bool isChanged = AuthenticationRepository.ChangePassword(userId: authenticationResult.Data.ID, newPasswordHash: request.NewPassword);
            if (!isChanged) Result.Failure(AuthenticationErrors.InvalidCredentials);

            return Result.Success();
        }
    }
}
