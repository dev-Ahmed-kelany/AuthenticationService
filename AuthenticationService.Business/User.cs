using AuthenticationService.Repository;
using AuthenticationService.Dtos.Users;

namespace AuthenticationService.Business
{
    public class User
    {
        public static int AddUser(CreateUserDto user)
        {
            return UserRepository.AddUser(user);
        }

        public static bool UpdateUserByID(int id, UpdateUserDto user)
        {
            return UserRepository.UpdateUserByID(id, user);
        }

        public static bool DeleteUserByID(int id)
        {
            return UserRepository.DeleteUserByID(id);
        }

        public static List<UserDetailsDto> SearchUsers(string searchText)
        {
            return UserRepository.SearchUsers(searchText);
        }

        public static List<UserDetailsDto> FilterUsersByRoleID(int roleId)
        {
            return UserRepository.FilterUsersByRoleID(roleId);
        }

        public static List<UserDetailsDto> FilterUsersByStatusID(int statusId)
        {
            return UserRepository.FilterUsersByStatusID(statusId);
        }

        public static List<UserDetailsDto> GetAllUsers()
        {
            return UserRepository.GetAllUsers();
        }

        public static UserDetailsDto? GetUserByID(int id)
        {
            return UserRepository.GetUserByID(id);
        }

    }
}
