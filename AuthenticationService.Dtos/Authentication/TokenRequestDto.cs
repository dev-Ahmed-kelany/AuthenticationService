using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.Authentication
{
    public class TokenRequestDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public long PermissionsMask { get; set; }
    }
}
