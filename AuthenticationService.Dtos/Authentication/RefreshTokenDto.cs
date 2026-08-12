using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.Authentication
{
    public class RefreshTokenDto
    {
        public int ID { get; set; }

        public string TokenHash { get; set; } = null!;

        public int UserID { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }
    }
}
