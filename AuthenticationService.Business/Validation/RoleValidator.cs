using AuthenticationService.Dtos.Permissions;
using AuthenticationService.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business.Validation
{
    public class RoleValidator
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

        static void ValidatePermissionsMask(long permissionsMask, ValidationResult result)
        {
            if (permissionsMask <= 0)
            {
                ValidationError error = new ValidationError("PermissionsMask", "PermissionsMask must be greater than zero.");
                result.Errors.Add(error);
            }

        }

        public static ValidationResult ValidateSave(SaveRoleDto role)
        {
            ValidationResult result = new ValidationResult();

            ValidateName(role.Name, result);
            ValidatePermissionsMask(role.PermissionsMask, result);

            return result;
        }
    }
}
