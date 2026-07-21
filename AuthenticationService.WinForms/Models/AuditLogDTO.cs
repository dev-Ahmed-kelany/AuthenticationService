namespace AuthenticationService.WinForms.Models
{
    public class AuditLogDTO
    {
        public int ID { get; set; }

        public int UserID { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;

        public int EntityID { get; set; }
        public string EntityName { get; set; } = null!;

        public int OperationTypeID { get; set; }
        public string OperationTypeName { get; set; } = null!;

        public DateTime DateTime { get; set; }
    }
}