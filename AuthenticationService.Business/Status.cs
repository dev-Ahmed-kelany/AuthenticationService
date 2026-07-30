using AuthenticationService.Repository;

namespace AuthenticationService.Business
{
    public class Status
    {
        public static async Task<bool> ExistsAsync(int statusId) { return await StatusRepository.ExistsAsync(statusId); }
    }
}
