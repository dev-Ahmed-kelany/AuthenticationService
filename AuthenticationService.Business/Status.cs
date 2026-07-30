using AuthenticationService.Repository;

namespace AuthenticationService.Business
{
    public class Status
    {
        public static bool StatusExists(int statusId) { return StatusRepository.StatusExists(statusId); }
    }
}
