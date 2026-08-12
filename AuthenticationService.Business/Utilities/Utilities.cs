
using System.Text;
using System.Security.Cryptography;


namespace AuthenticationService.Business.Utilities
{
    public class Utilities
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static string HashRefreshToken(string refreshToken)
        {
            byte[] tokenBytes = Encoding.UTF8.GetBytes(refreshToken);

            byte[] hashBytes = SHA256.HashData(tokenBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
