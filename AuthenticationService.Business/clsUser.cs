using System;
using AuthenticationService.Repository;

namespace AuthenticationService.Business
{
    public class clsUser
    {
        public static int AddNewUser(string Name, string Username, string Email,
            string PasswordHash, int RoleID, int StatusID)
        {
            return clsUserRepository.AddNewUser(Name, Username, Email, PasswordHash, RoleID, StatusID);
        }

        public static bool UpdateUserByID(int ID, string Name, string Username, string Email
            , int RoleID, int StatusID)
        {
            return clsUserRepository.UpdateUserByID(ID, Name, Username, Email, RoleID, StatusID);
        }

        public static bool DeleteUserByID(int ID)
        {
            return clsUserRepository.DeleteUserByID(ID);
        }

        public static List<UserDTO> SearchUsers(string SearchText)
        {
            return clsUserRepository.SearchUsers(SearchText);
        }

        public static List<UserDTO> FilterUsersByRoleID(int RoleID)
        {
            return clsUserRepository.FilterUsersByRoleID(RoleID);
        }

        public static List<UserDTO> FilterUsersByStatusID(int StatusID)
        {
            return clsUserRepository.FilterUsersByStatusID(StatusID);
        }

        public static List<UserDTO> GetAllUsers()
        {
            return clsUserRepository.GetAllUsers();
        }

        public static UserDTO? GetUserByID(int ID)
        {
            return clsUserRepository.GetUserByID(ID);
        }

    }
}
