using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
    public class AuthenticationUserDTO
    {
        public int ID { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public int RoleID { get; set; }

        public int StatusID { get; set; }
    }

    public class clsAuthenticationRepository
    {

    }
}
