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
    }
}
