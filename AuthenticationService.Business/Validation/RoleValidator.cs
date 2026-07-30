using AuthenticationService.Dtos.Roles;

namespace AuthenticationService.Business.Validation
{
    public static class RoleValidatorErrors
    {
        public static readonly Error RequiredName = new Error("Role.RequiredName", "Name is required.", HttpStatus.BadRequest);
        public static readonly Error InvalidNameLength = new Error("Role.InvalidNameLength", "Name cannot exceed 100 characters.", HttpStatus.BadRequest);
        public static readonly Error InvalidPermissionsMask = new Error("Role.InvalidPermissionsMask", "PermissionsMask must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error InvalidId = new Error("Role.InvalidId", "Id must be greater than zero.", HttpStatus.BadRequest);
    }

    public class RoleValidator
    {
        static Result ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure(RoleValidatorErrors.RequiredName);
            }

            if (!ValidationHelper.IsLengthBetween(name, 1, 100))
            {
                return Result.Failure(RoleValidatorErrors.InvalidNameLength);
            }

            return Result.Success();
        }

        static Result ValidatePermissionsMask(long permissionsMask)
        {
            if (permissionsMask <= 0)
                return Result.Failure(RoleValidatorErrors.InvalidPermissionsMask);

            return Result.Success();
        }

        static Result ValidateId(int id)
        {
            if (id <= 0)
                return Result.Failure(PermissionValidatorErrors.InvalidId);

            return Result.Success();
        }

        public static Result ValidateCreate(SaveRoleDto role)
        {
            var validateName = ValidateName(role.Name);
            if (!validateName.IsSuccess) return validateName;

            var validateBitValue = ValidatePermissionsMask(role.PermissionsMask);
            if (!validateBitValue.IsSuccess) return validateBitValue;

            return Result.Success();
        }

        public static Result ValidateUpdate(int id, SaveRoleDto role)
        {
            var validateId = ValidateId(id);
            if (!validateId.IsSuccess) return validateId;

            var validateName = ValidateName(role.Name);
            if (!validateName.IsSuccess) return validateName;

            var validateBitValue = ValidatePermissionsMask(role.PermissionsMask);
            if (!validateBitValue.IsSuccess) return validateBitValue;

            return Result.Success();
        }
    }
}
