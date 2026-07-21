namespace AuthenticationService.Desktop.Models
{
    public class AuditLogDTO
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; } = string.Empty;

        public int EntityID { get; set; }

        public string EntityName { get; set; } = string.Empty;

        public int OperationTypeID { get; set; }

        public string OperationTypeName { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }
    }
}