using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.Authentication
{
    public class AccessTokenResultDto
    {
        public string Token { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }
    }
}
