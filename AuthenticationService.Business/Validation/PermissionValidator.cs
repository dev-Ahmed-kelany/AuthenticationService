using AuthenticationService.Dtos.Permissions;
using AuthenticationService.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business.Validation
{
    public class PermissionValidator
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

        static void ValidateBitValue(long bitValue, ValidationResult result)
        {
            if (bitValue <= 0)
            {
                ValidationError error = new ValidationError("BitValue", "BitValue must be greater than zero.");
                result.Errors.Add(error);
            }

            if (!((bitValue & (bitValue - 1)) == 0))
            {
                result.AddError("BitValue", "BitValue is not a power of 2");
            }
        }

        public static ValidationResult ValidateCreate(CreatePermissionDto permission)
        {
            ValidationResult result = new ValidationResult();

            ValidateName(permission.Name, result);
            ValidateBitValue(permission.BitValue, result);

            return result;
        }

        public static ValidationResult ValidateUpdate(int id, UpdatePermissionDto permission)
        {
            ValidationResult result = new ValidationResult();

            ValidateName(permission.Name, result);

            return result;
        }
    }
}
