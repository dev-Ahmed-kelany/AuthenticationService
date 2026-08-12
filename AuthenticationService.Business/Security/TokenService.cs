using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationService.Business.Security
{
    public class TokenService
    {
        public static AccessTokenResultDto GenerateAccessToken(TokenRequestDto request)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserId.ToString()),
                new Claim(ClaimTypes.Name, request.Username.ToString()),
                new Claim(ClaimTypes.Role, request.RoleName.ToString()),
                new Claim("PermissionsMask", request.PermissionsMask.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expiresAt = DateTime.UtcNow.AddMinutes(30);

            var token = new JwtSecurityToken(
                issuer: "AuthenticationServiceApi",
                audience: "AuthenticationServiceApiUsers",
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            var result = new AccessTokenResultDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt
            };

            return result;
        }

        public static string GenerateRefreshToken()
        {
            byte[] randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }

    }
}
