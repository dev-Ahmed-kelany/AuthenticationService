using System;
using AuthenticationService.Repository;

namespace AuthenticationService.Business
{
    public class User
    {
        public static int AddUser(string name, string username, string email,
            string password, int roleId, int statusId)
        {
            return UserRepository.AddUser(name, username, email, password, roleId, statusId);
        }

        public static bool UpdateUserByID(int id, string name, string username, string email
            , int roleId, int statusId)
        {
            return UserRepository.UpdateUserByID(id, name, username, email, roleId, statusId);
        }

        public static bool DeleteUserByID(int id)
        {
            return UserRepository.DeleteUserByID(id);
        }

        public static List<UserDTO> SearchUsers(string searchText)
        {
            return UserRepository.SearchUsers(searchText);
        }

        public static List<UserDTO> FilterUsersByRoleID(int roleId)
        {
            return UserRepository.FilterUsersByRoleID(roleId);
        }

        public static List<UserDTO> FilterUsersByStatusID(int statusId)
        {
            return UserRepository.FilterUsersByStatusID(statusId);
        }

        public static List<UserDTO> GetAllUsers()
        {
            return UserRepository.GetAllUsers();
        }

        public static UserDTO? GetUserByID(int id)
        {
            return UserRepository.GetUserByID(id);
        }

    }
}
