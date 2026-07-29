using AuthenticationService.Dtos.Users;
using AuthenticationService.Repository;

namespace AuthenticationService.Business.Validation
{
    public static class UserValidator
    {
        static void ValidateName(string? name, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ValidationError error = new ValidationError("Name", "Name is required.");
                result.Errors.Add(error);
                return;
            }

            if (!ValidationHelper.IsLengthBetween(name, 1, 100))
            {
                ValidationError error = new ValidationError("Name", "Name cannot exceed 100 characters.");
                result.Errors.Add(error);
            }
        }

        static void ValidateUsername(string? username, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                ValidationError error = new ValidationError("Username", "Username is required.");
                result.Errors.Add(error);
                return;
            }

            if (!ValidationHelper.IsLengthBetween(username, 1, 50))
            {
                ValidationError error = new ValidationError("Username", "Username cannot exceed 50 characters.");
                result.Errors.Add(error);
            }

            if (!ValidationHelper.IsValidUsername(username))
            {
                ValidationError error = new ValidationError("Username", "Username is invalid.");
                result.Errors.Add(error);
            }
        }

        static void ValidateEmail(string? email, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ValidationError error = new ValidationError("Email", "Email is required.");
                result.Errors.Add(error);
                return;
            }

            if (!ValidationHelper.IsLengthBetween(email, 6, 255))
            {
                ValidationError error = new ValidationError("Email", "Email cannot be less than 6 characters and greater than 255 characters.");
                result.Errors.Add(error);
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                ValidationError error = new ValidationError("Email", "Email address is invalid.");
                result.Errors.Add(error);
            }
        }

        static void ValidatePassword(string? password, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                ValidationError error = new ValidationError("Password", "Password is required.");
                result.Errors.Add(error);
                return;
            }

            if (!ValidationHelper.IsLengthBetween(password, 8, 255))
            {
                ValidationError error = new ValidationError("Password", "Password cannot be less than 8 characters and greater than 255 characters.");
                result.Errors.Add(error);
            }

            if (!ValidationHelper.IsStrongPassword(password))
            {
                ValidationError error = new ValidationError("Password", "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");
                result.Errors.Add(error);
            }
        }

        static void ValidateId(int id, ValidationResult result)
        {
            if (id <= 0)
            {
                ValidationError error = new ValidationError("ID", "ID must be greater than zero.");
                result.Errors.Add(error);
            }
        }

        static void ValidateRoleId(int roleId, ValidationResult result)
        {
            if (roleId <= 0)
            {
                ValidationError error = new ValidationError("RoleID", "RoleID must be greater than zero.");
                result.Errors.Add(error);
            }
        }

        static void ValidateStatusId(int statusId, ValidationResult result)
        {
            if (statusId <= 0)
            {
                ValidationError error = new ValidationError("StatusID", "StatusID must be greater than zero.");
                result.Errors.Add(error);
            }
        }


        public static ValidationResult ValidateCreate(CreateUserDto user)
        {
            ValidationResult result = new ValidationResult();

            ValidateName(user.Name, result);
            ValidateUsername(user.Username, result);
            ValidateEmail(user.Email, result);
            ValidatePassword(user.Password, result);
            ValidateRoleId(user.RoleID, result);
            ValidateStatusId(user.StatusID, result);

            return result;
        }

        public static ValidationResult ValidateUpdate(int id, UpdateUserDto user)
        {
            ValidationResult result = new ValidationResult();

            ValidateId(id, result);
            ValidateName(user.Name, result);
            ValidateUsername(user.Username, result);
            ValidateEmail(user.Email, result);
            ValidateRoleId(user.RoleID, result);
            ValidateStatusId(user.StatusID, result);

            return result;
        }
    }
}
