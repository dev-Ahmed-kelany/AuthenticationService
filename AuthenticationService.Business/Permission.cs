using AuthenticationService.Repository;
using AuthenticationService.Dtos.Permissions;
using AuthenticationService.Business.Validation;

namespace AuthenticationService.Business
{
    public static class PermissionErrors
    {
        public static readonly Error NameAlreadyExists = new("Permission.NameAlreadyExists", "Name already exists.", HttpStatus.Conflict);
        public static readonly Error BitValueAlreadyExists = new("Permission.BitValueAlreadyExists", "BitValue already exists.", HttpStatus.Conflict);
        public static readonly Error NotCreated = new Error("Permission.NotCreated", "Permission not created successfully.", HttpStatus.InternalServerError);
        public static readonly Error NotUpdated = new Error("Permission.NotUpdated", "Permission not updated successfully.", HttpStatus.InternalServerError);
        public static readonly Error NotFound = new Error("Permission.NotFound", "Permission is not found.", HttpStatus.NotFound);
        public static readonly Error InvalidID = new Error("Permission.InvalidID", "ID must be greater than zero.", HttpStatus.BadRequest);
    }

    public class Permission
    {
        public static bool PermissionExists(int permissionID)
        {
            return PermissionRepository.PermissionExists(permissionID);
        }

        public static bool PermissionNameExists(string permissionName)
        {
            return PermissionRepository.PermissionNameExists(permissionName);
        }

        public static bool BitValueExists(long bitValue)
        {
            return PermissionRepository.BitValueExists(bitValue);
        }

        public static Result<int> AddPermission(CreatePermissionDto permission)
        {
            var validationResult = PermissionValidator.ValidateCreate(permission);
            if (!validationResult.IsSuccess) return new Result<int>(validationResult);

            if (PermissionNameExists(permission.Name)) return Result<int>.Failure(PermissionErrors.NameAlreadyExists);
            if (BitValueExists(permission.BitValue)) return Result<int>.Failure(PermissionErrors.BitValueAlreadyExists);

            int newPermissionId = PermissionRepository.AddPermission(permission);
            if (newPermissionId == -1) return Result<int>.Failure(PermissionErrors.NotCreated);

            return Result<int>.Success(newPermissionId);
        }

        public static Result UpdatePermissionByID(int id, UpdatePermissionDto permission)
        {
            var validationResult = PermissionValidator.ValidateUpdate(id, permission);
            if (!validationResult.IsSuccess) return validationResult;

            if (!PermissionExists(id)) return Result.Failure(PermissionErrors.NotFound);
            if (PermissionNameExists(permission.Name)) return Result.Failure(PermissionErrors.NameAlreadyExists);
         
            bool result = PermissionRepository.UpdatePermissionByID(id, permission);
            if (!result) return Result.Failure(PermissionErrors.NotUpdated);

            return Result.Success();
        }

        public static Result<List<PermissionDetailsDto>> SearchPermissionsByName(string searchText)
        {
            var permissionsList = PermissionRepository.SearchPermissionsByName(searchText);

            return Result<List<PermissionDetailsDto>>.Success(permissionsList);
        }

        public static Result<PermissionDetailsDto> GetPermissionByID(int id)
        {
            if (id <= 0) return Result<PermissionDetailsDto>.Failure(PermissionErrors.InvalidID);

            var permission = PermissionRepository.GetPermissionByID(id);
            if (permission == null) return Result<PermissionDetailsDto>.Failure(PermissionErrors.NotFound);

            return Result<PermissionDetailsDto>.Success(permission);
        }

        public static Result<List<PermissionDetailsDto>> GetAllPermissions()
        {
            var permissionsList = PermissionRepository.GetAllPermissions();

            return Result<List<PermissionDetailsDto>>.Success(permissionsList);
        }
    }
}
