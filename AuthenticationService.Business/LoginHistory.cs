using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public static class LoginHistory
    {
        public static int AddLoginHistory(LoginHistoryDTO loginHistory)
        {
            return LoginHistoryRepository.AddLoginHistory(loginHistory);
        }

        public static LoginHistoryDTO? Find(int id)
        {
            return LoginHistoryRepository.GetLoginHistoryByID(id);
        }

        public static List<LoginHistoryDTO> GetAll()
        {
            return LoginHistoryRepository.GetAllLoginHistory();
        }

        public static List<LoginHistoryDTO> GetByUserID(int userId)
        {
            return LoginHistoryRepository.GetLoginHistoryByUserID(userId);
        }

        public static List<LoginHistoryDTO> Search(string searchText)
        {
            return LoginHistoryRepository.SearchLoginHistory(searchText);
        }

        public static List<LoginHistoryDTO> FilterByStatus(byte status)
        {
            return LoginHistoryRepository.FilterLoginHistoryByStatus(status);
        }
    }
}
