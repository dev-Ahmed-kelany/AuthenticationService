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
    }
}
