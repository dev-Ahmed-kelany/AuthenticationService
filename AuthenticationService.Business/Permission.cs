using AuthenticationService.Repository;
using AuthenticationService.Dtos.Permissions;

namespace AuthenticationService.Business
{
    public class Permission
    {
        public static int AddPermission(CreatePermissionDto permission)
        {
            return PermissionRepository.AddPermission(permission);
        }

        public static bool UpdatePermissionByID(int id, UpdatePermissionDto permission)
        {
            return PermissionRepository.UpdatePermissionByID(id, permission);
        }

        public static List<PermissionDetailsDto> SearchPermissionsByName(string searchText)
        {
            return PermissionRepository.SearchPermissionsByName(searchText);
        }

        public static PermissionDetailsDto? GetPermissionByID(int id)
        {
            return PermissionRepository.GetPermissionByID(id);
        }
        public static List<PermissionDetailsDto> GetAllPermissions()
        {
            return PermissionRepository.GetAllPermissions();
        }
    }
}
