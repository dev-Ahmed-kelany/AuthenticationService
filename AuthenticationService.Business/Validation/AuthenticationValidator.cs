using AuthenticationService.Dtos.Authentication;

namespace AuthenticationService.Business.Validation
{
    public static class AuthenticationValidatorErrors
    {
        public static readonly Error RequiredName = new Error("User.RequiredName", "Name is required.", HttpStatus.BadRequest);
        public static readonly Error InvalidNameLength = new Error("User.InvalidNameLength", "Name cannot exceed 100 characters.", HttpStatus.BadRequest);

        public static readonly Error RequiredUsername = new Error("User.RequiredUsername", "Username is required.", HttpStatus.BadRequest);
        public static readonly Error InvalidUsernameLength = new Error("User.InvalidUsernameLength", "Username cannot exceed 50 characters.", HttpStatus.BadRequest);
        public static readonly Error InvalidUsername = new Error("User.InvalidUsername", "Username is invalid.", HttpStatus.BadRequest);

        public static readonly Error RequiredEmail = new Error("User.RequiredEmail", "Email is required.", HttpStatus.BadRequest);
        public static readonly Error InvalidEmailLength = new Error("User.InvalidEmailLength", "Email cannot be less than 6 characters and greater than 255 characters.", HttpStatus.BadRequest);
        public static readonly Error InvalidEmail = new Error("User.InvalidEmail", "Email address is invalid.", HttpStatus.BadRequest);

        public static readonly Error RequiredPassword = new Error("User.RequiredPassword", "Password is required.", HttpStatus.BadRequest);
        public static readonly Error InvalidPasswordLength = new Error("User.InvalidPasswordLength", "Password cannot be less than 8 characters and greater than 255 characters.", HttpStatus.BadRequest);
        public static readonly Error InvalidPassword = new Error("User.InvalidPassword", "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.", HttpStatus.BadRequest);

        public static readonly Error InvalidId = new Error("User.InvalidId", "Id must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error InvalidRoleId = new Error("User.InvalidRoleId", "RoleId must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error InvalidStatusId = new Error("User.InvalidStatusId", "StatusId must be greater than zero.", HttpStatus.BadRequest);
    }
    public class AuthenticationValidator
    {
        static Result ValidateUsername(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Result.Failure(UserValidatorErrors.RequiredUsername);

            if (!ValidationHelper.IsLengthBetween(username, 1, 50))
                return Result.Failure(UserValidatorErrors.InvalidUsernameLength);

            if (!ValidationHelper.IsValidUsername(username))
                return Result.Failure(UserValidatorErrors.InvalidUsername);

            return Result.Success();
        }

        static Result ValidatePassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return Result.Failure(UserValidatorErrors.RequiredPassword);

            if (!ValidationHelper.IsLengthBetween(password, 8, 255))
                return Result.Failure(UserValidatorErrors.InvalidPasswordLength);

            return Result.Success();
        }

        public static Result ValidateAuthenticate(AuthenticationRequestDto request)
        {
            var validateUsername = ValidateUsername(request.Username);
            if (!validateUsername.IsSuccess) return validateUsername;

            var validatePassword = ValidatePassword(request.Password);
            if (!validatePassword.IsSuccess) return validatePassword;

            return Result.Success();
        }

        public static Result ValidateChangePassword(ChangePasswordDto request)
        {
            var validateUsername = ValidateUsername(request.Username);
            if (!validateUsername.IsSuccess) return validateUsername;

            var validateCurrentPassword = ValidatePassword(request.CurrentPassword);
            if (!validateCurrentPassword.IsSuccess) return validateCurrentPassword;

            var validateNewPassword = ValidatePassword(request.NewPassword);
            if (!validateNewPassword.IsSuccess) return validateNewPassword;

            return Result.Success();
        }
    }
}
