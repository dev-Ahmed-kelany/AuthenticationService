using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Dtos.AuditLogs
{
    public class CreateAuditLogDto
    {
        public int UserID { get; set; }
        public int EntityID { get; set; }
        public int OperationTypeID { get; set; }
    }
}
