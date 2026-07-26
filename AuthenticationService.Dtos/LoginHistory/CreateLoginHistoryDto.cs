using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.LoginHistory
{
    public class CreateLoginHistoryDto
    {
        public int? UserID { get; set; }

        public byte Status { get; set; }

        public string? FailureReason { get; set; }

        public string? IPAddress { get; set; }

        public string? Device { get; set; }

        public string? Browser { get; set; }
    }
}
