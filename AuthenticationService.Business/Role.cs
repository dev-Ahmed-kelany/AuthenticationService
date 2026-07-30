using AuthenticationService.Dtos.Roles;
using AuthenticationService.Repository;
using AuthenticationService.Business.Validation;

namespace AuthenticationService.Business
{
    public static class RoleErrors
    {
        public static readonly Error NameAlreadyExists = new("Role.NameAlreadyExists", "Name already exists.", HttpStatus.Conflict);
        public static readonly Error NotCreated = new Error("Role.NotCreated", "Role not created successfully.", HttpStatus.InternalServerError);
        public static readonly Error NotUpdated = new Error("Role.NotUpdated", "Role not updated successfully.", HttpStatus.InternalServerError);
        public static readonly Error NotFound = new Error("Role.NotFound", "Role is not found.", HttpStatus.NotFound);
        public static readonly Error InvalidID = new Error("Role.InvalidID", "ID must be greater than zero.", HttpStatus.BadRequest);
    }

    public class Role
    {
        public static async Task<bool> ExistsAsync(int roleId) { return await RoleRepository.ExistsAsync(roleId); }

        public static async Task<bool> RoleNameExistsAsync(string roleName) { return await RoleRepository.RoleNameExistsAsync(roleName); }
        private static async Task<bool> RoleNameExistsAsync(string roleName, int excludeRoleId) { return await RoleRepository.RoleNameExistsAsync(roleName, excludeRoleId); }

        public static async Task<Result<int>> AddAsync(SaveRoleDto role)
        {
            var validationResult = RoleValidator.ValidateCreate(role);
            if (!validationResult.IsSuccess) return new Result<int>(validationResult);

            if (await RoleNameExistsAsync(role.Name)) return Result<int>.Failure(RoleErrors.NameAlreadyExists);

            var newRoleId = await RoleRepository.AddAsync(role);
            if (newRoleId == -1) return Result<int>.Failure(RoleErrors.NotCreated);

            return Result<int>.Success(newRoleId);
        }

        public static async Task<Result> UpdateByIDAsync(int id, SaveRoleDto role)
        {
            var validationResult = RoleValidator.ValidateUpdate(id, role);
            if (!validationResult.IsSuccess) return validationResult;

            if (!await ExistsAsync(id)) return Result.Failure(RoleErrors.NotFound);
            if (await RoleNameExistsAsync(role.Name, id)) return Result.Failure(RoleErrors.NameAlreadyExists);

            bool result = await RoleRepository.UpdateByIDAsync(id, role);
            if (!result) return Result.Failure(RoleErrors.NotUpdated);

            return Result.Success();
        }

        public static async Task<Result<List<RoleDetailsDto>>> SearchByNameAsync(string searchText)
        {
            var rolesList = await RoleRepository.SearchByNameAsync(searchText);
            return Result<List<RoleDetailsDto>>.Success(rolesList);
        }

        public static async Task<Result<RoleDetailsDto>> GetByIDAsync(int id)
        {
            if (id <= 0) return Result<RoleDetailsDto>.Failure(RoleErrors.InvalidID);

            var role = await RoleRepository.GetByIDAsync(id);
            if (role == null) return Result<RoleDetailsDto>.Failure(RoleErrors.NotFound);

            return Result<RoleDetailsDto>.Success(role);
        }

        public static async Task<Result<List<RoleDetailsDto>>> GetAllAsync()
        {
            var rolesList = await RoleRepository.GetAllAsync();
            return Result<List<RoleDetailsDto>>.Success(rolesList);
        }

    }
}
