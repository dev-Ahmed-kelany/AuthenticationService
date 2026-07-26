using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.Permissions
{
    public class PermissionDetailsDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public long BitValue { get; set; }
    }
}
