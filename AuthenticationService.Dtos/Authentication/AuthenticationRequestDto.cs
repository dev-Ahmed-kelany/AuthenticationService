using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.Authentication
{
    public class AuthenticationRequestDto

    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
