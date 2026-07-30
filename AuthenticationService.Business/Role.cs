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
        public static bool RoleExists(int roleId) { return RoleRepository.RoleExists(roleId); }

        public static bool RoleNameExists(string roleName) { return RoleRepository.RoleNameExists(roleName); }
        private static bool RoleNameExists(string roleName, int excludeRoleId) { return RoleRepository.RoleNameExists(roleName, excludeRoleId); }

        public static Result<int> AddRole(SaveRoleDto role)
        {
            var validationResult = RoleValidator.ValidateCreate(role);
            if (!validationResult.IsSuccess) return new Result<int>(validationResult);

            if (RoleNameExists(role.Name)) return Result<int>.Failure(RoleErrors.NameAlreadyExists);

            var newRoleId = RoleRepository.AddRole(role);
            if (newRoleId == -1) return Result<int>.Failure(RoleErrors.NotCreated);

            return Result<int>.Success(newRoleId);
        }

        public static Result UpdateRoleByID(int id, SaveRoleDto role)
        {
            var validationResult = RoleValidator.ValidateUpdate(id, role);
            if (!validationResult.IsSuccess) return validationResult;

            if (!RoleExists(id)) return Result.Failure(RoleErrors.NotFound);
            if (RoleNameExists(role.Name, id)) return Result.Failure(RoleErrors.NameAlreadyExists);

            bool result = RoleRepository.UpdateRoleByID(id, role);
            if (!result) return Result.Failure(RoleErrors.NotUpdated);

            return Result.Success();
        }

        public static Result<List<RoleDetailsDto>> SearchRolesByName(string searchText)
        {
            var rolesList = RoleRepository.SearchRolesByName(searchText);
            return Result<List<RoleDetailsDto>>.Success(rolesList);
        }

        public static Result<RoleDetailsDto> GetRoleByID(int id)
        {
            if (id <= 0) return Result<RoleDetailsDto>.Failure(RoleErrors.InvalidID);

            var role = RoleRepository.GetRoleByID(id);
            if (role == null) return Result<RoleDetailsDto>.Failure(RoleErrors.NotFound);

            return Result<RoleDetailsDto>.Success(role);
        }

        public static Result<List<RoleDetailsDto>> GetAllRoles()
        {
            var rolesList = RoleRepository.GetAllRoles();
            return Result<List<RoleDetailsDto>>.Success(rolesList);
        }

    }
}
