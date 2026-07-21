namespace AuthenticationService.Desktop.Models
{
    public class RoleDTO
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public long PermissionsMask { get; set; }
    }
}