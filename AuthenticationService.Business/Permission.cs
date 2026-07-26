using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public class Permission
    {
        public static int AddPermission(string name, long bitValue)
        {
            return PermissionRepository.AddPermission(name, bitValue);
        }
        public static bool UpdatePermissionByID(int id, string name)
        {
            return PermissionRepository.UpdatePermissionByID(id, name);
        }

        public static List<PermissionDTO> SearchPermissionsByName(string searchText)
        {
            return PermissionRepository.SearchPermissionsByName(searchText);
        }

        public static PermissionDTO? GetPermissionByID(int id)
        {
            return PermissionRepository.GetPermissionByID(id);
        }
        public static List<PermissionDTO> GetAllPermissions()
        {
            return PermissionRepository.GetAllPermissions();
        }
    }
}
