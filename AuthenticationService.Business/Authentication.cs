using AuthenticationService.Repository;
using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Business.Validation;
using System.Threading.Tasks;

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

        public static async Task<Result> LoginAsync(AuthenticationRequestDto request) { return await AuthenticateUserAsync(request); }

        public static async Task<Result> VerifyCredentialsAsync(AuthenticationRequestDto request) { return await AuthenticateUserAsync(request); }

        public static async Task<Result> ChangePasswordAsync(ChangePasswordDto request)
        {
            var authenticationResult = await AuthenticateUserAsync(new AuthenticationRequestDto { Username = request.Username, Password = request.CurrentPassword});
            if (!authenticationResult.IsSuccess) return authenticationResult;

            bool isChanged = await AuthenticationRepository.ChangePasswordAsync(userId: authenticationResult.Data.ID, newPasswordHash: request.NewPassword);
            if (!isChanged) Result.Failure(AuthenticationErrors.InvalidCredentials);

            return Result.Success();
        }
    }
}
