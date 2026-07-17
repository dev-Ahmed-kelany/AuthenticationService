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

    }
}
