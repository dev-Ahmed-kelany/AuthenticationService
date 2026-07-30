using AuthenticationService.Dtos.Users;

namespace AuthenticationService.Business.Validation
{
    public static class UserValidatorErrors
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

    public static class UserValidator
    {
        static Result ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure(UserValidatorErrors.RequiredName);
            }

            if (!ValidationHelper.IsLengthBetween(name, 1, 100))
            {
                return Result.Failure(UserValidatorErrors.InvalidNameLength);
            }

            return Result.Success();
        }

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

        static Result ValidateEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure(UserValidatorErrors.RequiredEmail);

            if (!ValidationHelper.IsLengthBetween(email, 6, 255))
                return Result.Failure(UserValidatorErrors.InvalidEmailLength);

            if (!ValidationHelper.IsValidEmail(email))
                return Result.Failure(UserValidatorErrors.InvalidEmail);

            return Result.Success();
        }

        static Result ValidatePassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return Result.Failure(UserValidatorErrors.RequiredPassword);

            if (!ValidationHelper.IsLengthBetween(password, 8, 255))
                return Result.Failure(UserValidatorErrors.InvalidPasswordLength);

            if (!ValidationHelper.IsStrongPassword(password))
                return Result.Failure(UserValidatorErrors.InvalidPassword);

            return Result.Success();
        }

        public static Result ValidateId(int id)
        {
            if (id <= 0)
                return Result.Failure(UserValidatorErrors.InvalidId);

            return Result.Success();
        }

        public static Result ValidateRoleId(int roleId)
        {
            if (roleId <= 0)
                return Result.Failure(UserValidatorErrors.InvalidRoleId);

            return Result.Success();
        }

        public static Result ValidateStatusId(int statusId)
        {
            if (statusId <= 0)
                return Result.Failure(UserValidatorErrors.InvalidStatusId);

            return Result.Success();
        }

        public static Result ValidateCreate(CreateUserDto user)
        {
            var validateName = ValidateName(user.Name);
            if (!validateName.IsSuccess) return validateName;

            var validateUsername = ValidateUsername(user.Username);
            if (!validateUsername.IsSuccess) return validateUsername;

            var validateEmail = ValidateEmail(user.Email);
            if (!validateEmail.IsSuccess) return validateEmail;

            var validatePassword = ValidatePassword(user.Password);
            if (!validatePassword.IsSuccess) return validatePassword;

            var validateRoleId = ValidateRoleId(user.RoleID);
            if (!validateRoleId.IsSuccess) return validateRoleId;

            var validateStatusId = ValidateStatusId(user.StatusID);
            if (!validateStatusId.IsSuccess) return validateStatusId;

            return Result.Success();
        }

        public static Result ValidateUpdate(int id, UpdateUserDto user)
        {
            var validateId = ValidateId(id);
            if (!validateId.IsSuccess) return validateId;

            var validateName = ValidateName(user.Name);
            if (!validateName.IsSuccess) return validateName;

            var validateUsername = ValidateUsername(user.Username);
            if (!validateUsername.IsSuccess) return validateUsername;

            var validateEmail = ValidateEmail(user.Email);
            if (!validateEmail.IsSuccess) return validateEmail;

            var validateRoleId = ValidateRoleId(user.RoleID);
            if (!validateRoleId.IsSuccess) return validateRoleId;

            var validateStatusId = ValidateStatusId(user.StatusID);
            if (!validateStatusId.IsSuccess) return validateStatusId;

            return Result.Success();
        }
    }
}
