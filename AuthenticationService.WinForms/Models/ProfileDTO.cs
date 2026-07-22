namespace AuthenticationService.WinForms.Models
{
    public class ProfileDTO
    {
        public int ID { get; set; }

        public string Name { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string RoleName { get; set; } = null!;

        public string StatusName { get; set; } = null!;
    }
}