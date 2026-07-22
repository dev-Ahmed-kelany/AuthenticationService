namespace AuthenticationService.WinForms.Models
{
    public class UserDTO
    {
        // Properties
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleID { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}