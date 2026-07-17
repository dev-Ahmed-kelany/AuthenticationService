using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public class clsPermission
    {
        public static int AddNewPermission(string Name, long BitValue)
        {
            return clsPermissionRepository.AddNewPermission(Name, BitValue);
        }
        public static bool UpdatePermissionByID(int ID, string Name)
        {
            return clsPermissionRepository.UpdatePermissionByID(ID, Name);
        }

        public static List<PermissionDTO> SearchPermissionsByName(string SearchText)
        {
            return clsPermissionRepository.SearchPermissionsByName(SearchText);
        }

        public static PermissionDTO? GetPermissionByID(int ID)
        {
            return clsPermissionRepository.GetPermissionByID(ID);
        }
        public static List<PermissionDTO> GetAllPermissions()
        {
            return clsPermissionRepository.GetAllPermissions();
        }
    }
}
