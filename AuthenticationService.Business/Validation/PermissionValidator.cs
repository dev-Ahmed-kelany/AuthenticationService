using AuthenticationService.Dtos.Permissions;

namespace AuthenticationService.Business.Validation
{
    public static class PermissionValidatorErrors
    {
        public static readonly Error RequiredName = new Error("Permission.RequiredName", "Name is required.", HttpStatus.BadRequest);
        public static readonly Error InvalidNameLength = new Error("Permission.InvalidNameLength", "Name cannot exceed 100 characters.", HttpStatus.BadRequest);
        public static readonly Error InvalidBitValue = new Error("Permission.InvalidBitValue", "BitValue must be greater than zero & Power of 2.", HttpStatus.BadRequest);
        public static readonly Error InvalidId = new Error("Permission.InvalidId", "Id must be greater than zero.", HttpStatus.BadRequest);
    }

    public class PermissionValidator
    {
        static Result ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure(PermissionValidatorErrors.RequiredName);

            if (!ValidationHelper.IsLengthBetween(name, 1, 100))
                return Result.Failure(PermissionValidatorErrors.InvalidNameLength);

            return Result.Success();
        }

        static Result ValidateBitValue(long bitValue)
        {
            if (bitValue <= 0)
                return Result.Failure(PermissionValidatorErrors.InvalidBitValue);

            if (!((bitValue & (bitValue - 1)) == 0))
                return Result.Failure(PermissionValidatorErrors.InvalidBitValue);

            return Result.Success();
        }

        static Result ValidateId(int id)
        {
            if (id <= 0)
                return Result.Failure(PermissionValidatorErrors.InvalidId);

            return Result.Success();
        }

        public static Result ValidateCreate(CreatePermissionDto permission)
        {
            var validateName = ValidateName(permission.Name);
            if (!validateName.IsSuccess) return validateName;

            var validateBitValue = ValidateBitValue(permission.BitValue);
            if (!validateBitValue.IsSuccess) return validateBitValue;

            return Result.Success();
        }

        public static Result ValidateUpdate(int id, UpdatePermissionDto permission)
        {
            var validateId = ValidateId(id);
            if (!validateId.IsSuccess) return validateId;

            var validateName = ValidateName(permission.Name);
            if (!validateName.IsSuccess) return validateName;

            return Result.Success();
        }
    }
}
