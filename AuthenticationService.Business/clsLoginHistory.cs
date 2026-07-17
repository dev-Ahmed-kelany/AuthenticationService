using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public static class clsLoginHistory
    {
        public static int AddNewLoginHistory(LoginHistoryDTO LoginHistory)
        {
            // Future business rules go here

            return clsLoginHistoryRepository.AddNewLoginHistory(LoginHistory);
        }

        public static LoginHistoryDTO? Find(int ID)
        {
            return clsLoginHistoryRepository.GetLoginHistoryByID(ID);
        }

        public static List<LoginHistoryDTO> GetAll()
        {
            return clsLoginHistoryRepository.GetAllLoginHistory();
        }

        public static List<LoginHistoryDTO> GetByUserID(int UserID)
        {
            return clsLoginHistoryRepository.GetLoginHistoryByUserID(UserID);
        }

        public static List<LoginHistoryDTO> Search(string SearchText)
        {
            return clsLoginHistoryRepository.SearchLoginHistory(SearchText);
        }

        public static List<LoginHistoryDTO> FilterByStatus(byte Status)
        {
            return clsLoginHistoryRepository.FilterLoginHistoryByStatus(Status);
        }
    }
}
