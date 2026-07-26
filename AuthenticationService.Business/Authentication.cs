using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public enum enAuthenticationResult
    {
        Success = 1,
        InvalidCredentials = 2,
        InactiveAccount = 3,
        LockedAccount = 4,
        DeletedAccount = 5
    }

    public class Authentication
    {

        private static enAuthenticationResult AuthenticateUser(string username,
                                                  string password,
                                                  ref AuthenticationUserDTO user)
        {
            bool IsFound = AuthenticationRepository.GetAuthenticationUserByUsername(username, ref user);

            if (!IsFound)
                return enAuthenticationResult.InvalidCredentials;

            if (password != user.PasswordHash)
                return enAuthenticationResult.InvalidCredentials;

            if (user.StatusID != 1)
                return enAuthenticationResult.InactiveAccount;

            return enAuthenticationResult.Success;
        }

        public static enAuthenticationResult Login(string username, string password)
        {
            AuthenticationUserDTO user = new AuthenticationUserDTO();

            return AuthenticateUser(username, password, ref user);
        }

        public static enAuthenticationResult VerifyCredentials(string username, string password)
        {
            AuthenticationUserDTO user = new AuthenticationUserDTO();

            return AuthenticateUser(username, password, ref user);
        }

        public static enAuthenticationResult ChangePassword(string username,
                                                   string currentPassword,
                                                   string newPassword)
        {
            AuthenticationUserDTO user = new AuthenticationUserDTO();

            enAuthenticationResult result = AuthenticateUser(username, currentPassword, ref user);

            if (result != enAuthenticationResult.Success)
                return result;

            bool IsChanged = AuthenticationRepository.ChangePassword(user.ID, newPassword);

            if (!IsChanged)
                return enAuthenticationResult.InvalidCredentials;

            return enAuthenticationResult.Success;
        }
    }
}
