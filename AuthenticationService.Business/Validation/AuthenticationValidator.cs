using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business.Validation
{
    public class AuthenticationValidator
    {
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

        static void ValidatePassword(string? password, string property, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                ValidationError error = new ValidationError(property, $"{property} is required.");
                result.Errors.Add(error);
                return;
            }

            if (!ValidationHelper.IsLengthBetween(password, 8, 255))
            {
                ValidationError error = new ValidationError(property, $"{property} cannot be less than 8 characters and greater than 255 characters.");
                result.Errors.Add(error);
            }
        }

        public static ValidationResult ValidateLogin(LoginRequestDto request)
        {
            ValidationResult result = new ValidationResult();

            ValidateUsername(request.Username, result);
            ValidatePassword(request.Password, "Password", result);

            return result;
        }

        public static ValidationResult ValidateChangePassword(ChangePasswordDto request)
        {
            ValidationResult result = new ValidationResult();

            ValidateUsername(request.Username, result);
            ValidatePassword(request.CurrentPassword, "CurrentPassword", result);
            ValidatePassword(request.NewPassword, "NewPassword", result);

            return result;
        }
    }
}
