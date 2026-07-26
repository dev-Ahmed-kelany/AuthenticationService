using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public class Role
    {
        public static int AddRole(string name, long permissionsMask)
        {
            return RoleRepository.AddRole(name, permissionsMask);
        }
        public static bool UpdateRoleByID(int id, string name, long permissionsMask)
        {
            return RoleRepository.UpdateRoleByID(id, name, permissionsMask);
        }

        public static List<RoleDTO> SearchRolesByName(string searchText)
        {
            return RoleRepository.SearchRolesByName(searchText);
        }

        public static RoleDTO? GetRoleByID(int id)
        {
            return RoleRepository.GetRoleByID(id);
        }
        public static List<RoleDTO> GetAllRoles()
        {
            return RoleRepository.GetAllRoles();
        }


    }
}
