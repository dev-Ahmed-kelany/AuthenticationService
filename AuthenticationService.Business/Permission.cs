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
        public static async Task<bool> ExistsAsync(int permissionID) { return await PermissionRepository.ExistsAsync(permissionID); }

        public static async Task<bool> PermissionNameExistsAsync(string permissionName) { return await PermissionRepository.PermissionNameExistsAsync(permissionName); }

        public static async Task<bool> BitValueExistsAsync(long bitValue) { return await PermissionRepository.BitValueExistsAsync(bitValue); }

        public static async Task<Result<int>> AddAsync(CreatePermissionDto permission)
        {
            var validationResult = PermissionValidator.ValidateCreate(permission);
            if (!validationResult.IsSuccess) return new Result<int>(validationResult);

            if (await PermissionNameExistsAsync(permission.Name)) return Result<int>.Failure(PermissionErrors.NameAlreadyExists);
            if (await BitValueExistsAsync(permission.BitValue)) return Result<int>.Failure(PermissionErrors.BitValueAlreadyExists);

            int newPermissionId = await PermissionRepository.AddAsync(permission);
            if (newPermissionId == -1) return Result<int>.Failure(PermissionErrors.NotCreated);

            return Result<int>.Success(newPermissionId);
        }

        public static async Task<Result> UpdateByIDAsync(int id, UpdatePermissionDto permission)
        {
            var validationResult = PermissionValidator.ValidateUpdate(id, permission);
            if (!validationResult.IsSuccess) return validationResult;

            if (!await ExistsAsync(id)) return Result.Failure(PermissionErrors.NotFound);
            if (await PermissionNameExistsAsync(permission.Name)) return Result.Failure(PermissionErrors.NameAlreadyExists);
         
            bool result = await PermissionRepository.UpdateByIDAsync(id, permission);
            if (!result) return Result.Failure(PermissionErrors.NotUpdated);

            return Result.Success();
        }

        public static async Task<Result<List<PermissionDetailsDto>>> SearchByNameAsync(string searchText)
        {
            var permissionsList = await PermissionRepository.SearchByNameAsync(searchText);

            return Result<List<PermissionDetailsDto>>.Success(permissionsList);
        }

        public static async Task<Result<PermissionDetailsDto>> GetByIDAsync(int id)
        {
            if (id <= 0) return Result<PermissionDetailsDto>.Failure(PermissionErrors.InvalidID);

            var permission = await PermissionRepository.GetByIDAsync(id);
            if (permission == null) return Result<PermissionDetailsDto>.Failure(PermissionErrors.NotFound);

            return Result<PermissionDetailsDto>.Success(permission);
        }

        public static async Task<Result<List<PermissionDetailsDto>>> GetAllAsync()
        {
            var permissionsList = await PermissionRepository.GetAllAsync();

            return Result<List<PermissionDetailsDto>>.Success(permissionsList);
        }
    }
}
