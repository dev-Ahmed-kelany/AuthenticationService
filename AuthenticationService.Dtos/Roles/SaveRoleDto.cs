using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.Roles
{
    public class SaveRoleDto
    {
        public string Name { get; set; } = null!;
        public long PermissionsMask { get; set; }
    }
}
