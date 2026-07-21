namespace AuthenticationService.WinForms.Models
{
    public class ProfileDTO
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public int StatusID { get; set; }

        public string StatusName { get; set; } = string.Empty;
    }
}