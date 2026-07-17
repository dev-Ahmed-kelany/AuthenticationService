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

    public class clsAuthentication
    {

        private static enAuthenticationResult AuthenticateUser(string Username,
                                                  string Password,
                                                  ref AuthenticationUserDTO User)
        {
            bool IsFound = clsAuthenticationRepository.GetAuthenticationUserByUsername(Username, ref User);

            if (!IsFound)
                return enAuthenticationResult.InvalidCredentials;

            if (Password != User.PasswordHash)
                return enAuthenticationResult.InvalidCredentials;

            if (User.StatusID != 1)
                return enAuthenticationResult.InactiveAccount;

            return enAuthenticationResult.Success;
        }

        public static enAuthenticationResult Login(string Username, string Password)
        {
            AuthenticationUserDTO User = new AuthenticationUserDTO();

            return AuthenticateUser(Username, Password, ref User);
        }

        public static enAuthenticationResult VerifyCredentials(string Username, string Password)
        {
            AuthenticationUserDTO User = new AuthenticationUserDTO();

            return AuthenticateUser(Username, Password, ref User);
        }

        public static enAuthenticationResult ChangePassword(string Username,
                                                   string CurrentPassword,
                                                   string NewPassword)
        {
            AuthenticationUserDTO User = new AuthenticationUserDTO();

            enAuthenticationResult Result = AuthenticateUser(Username, CurrentPassword, ref User);

            if (Result != enAuthenticationResult.Success)
                return Result;

            bool IsChanged = clsAuthenticationRepository.ChangePassword(User.ID, NewPassword);

            if (!IsChanged)
                return enAuthenticationResult.InvalidCredentials;

            return enAuthenticationResult.Success;
        }
    }
}
