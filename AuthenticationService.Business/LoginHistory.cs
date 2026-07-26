using AuthenticationService.Repository;
using AuthenticationService.Dtos.LoginHistory;

namespace AuthenticationService.Business
{
    public static class LoginHistory
    {
        public static int AddLoginHistory(CreateLoginHistoryDto loginHistory)
        {
            return LoginHistoryRepository.AddLoginHistory(loginHistory);
        }

        public static LoginHistoryDetailsDto? Find(int id)
        {
            return LoginHistoryRepository.GetLoginHistoryByID(id);
        }

        public static List<LoginHistoryDetailsDto> GetAll()
        {
            return LoginHistoryRepository.GetAllLoginHistory();
        }

        public static List<LoginHistoryDetailsDto> GetByUserID(int userId)
        {
            return LoginHistoryRepository.GetLoginHistoryByUserID(userId);
        }

        public static List<LoginHistoryDetailsDto> Search(string searchText)
        {
            return LoginHistoryRepository.SearchLoginHistory(searchText);
        }

        public static List<LoginHistoryDetailsDto> FilterByStatus(byte status)
        {
            return LoginHistoryRepository.FilterLoginHistoryByStatus(status);
        }
    }
}
