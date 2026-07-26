using AuthenticationService.Repository;
using AuthenticationService.Dtos.Roles;

namespace AuthenticationService.Business
{
    public class Role
    {
        public static int AddRole(SaveRoleDto role)
        {
            return RoleRepository.AddRole(role);
        }

        public static bool UpdateRoleByID(int id, SaveRoleDto role)
        {
            return RoleRepository.UpdateRoleByID(id, role);
        }

        public static List<RoleDetailsDto> SearchRolesByName(string searchText)
        {
            return RoleRepository.SearchRolesByName(searchText);
        }

        public static RoleDetailsDto? GetRoleByID(int id)
        {
            return RoleRepository.GetRoleByID(id);
        }
        public static List<RoleDetailsDto> GetAllRoles()
        {
            return RoleRepository.GetAllRoles();
        }


    }
}
