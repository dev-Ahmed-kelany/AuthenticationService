
namespace AuthenticationService.Dtos.Authentication
{
    public class AuthenticatedUserDto
    {
        public int ID { get; set; }

        public string Username { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public long PermissionsMask { get; set; }

        public int StatusID { get; set; }
    }
}
