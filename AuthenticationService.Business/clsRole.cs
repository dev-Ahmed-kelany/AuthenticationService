using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public class clsRole
    {
        public static int AddNewRole(string Name, long PermissionsMask)
        {
            return clsRoleRepository.AddNewRole(Name, PermissionsMask);
        }
        public static bool UpdateRoleByID(int ID, string Name, long PermissionsMask)
        {
            return clsRoleRepository.UpdateRoleByID(ID, Name, PermissionsMask);
        }

        public static List<RoleDTO> SearchRolesByName(string SearchText)
        {
            return clsRoleRepository.SearchRolesByName(SearchText);
        }

        public static RoleDTO? GetRoleByID(int ID)
        {
            return clsRoleRepository.GetRoleByID(ID);
        }
        public static List<RoleDTO> GetAllRoles()
        {
            return clsRoleRepository.GetAllRoles();
        }


    }
}
