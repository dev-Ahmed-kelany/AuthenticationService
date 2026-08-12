using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Repository;

namespace AuthenticationService.Business
{
    public static class RefreshToken
    {
        public static async Task<int> CreateAsync(
            string tokenHash,
            int userId,
            DateTime expiresAt)
        {
            return await RefreshTokenRepository.CreateAsync(
                tokenHash,
                userId,
                expiresAt);
        }

        public static async Task<RefreshTokenDto?> GetByHashAsync(
            string tokenHash)
        {
            return await RefreshTokenRepository.GetByHashAsync(
                tokenHash);
        }

        public static async Task<bool> RevokeAsync(int id)
        {
            return await RefreshTokenRepository.RevokeAsync(id);
        }
    }
}