using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.Authentication
{
    public class LoginResponseDto
    {
        public int ID { get; set; }

        public string Username { get; set; } = null!;

        public int RoleID { get; set; }
    }
}
