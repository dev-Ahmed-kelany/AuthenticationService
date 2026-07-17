using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public enum enLoginResult
    {
        Success = 1,
        InvalidCredentials = 2,
        InactiveAccount = 3,
        LockedAccount = 4,
        DeletedAccount = 5
    }

    public class clsAuthentication
    {
        

        public static enLoginResult Login(string Username, string Password)
        {
            AuthenticationUserDTO User = new AuthenticationUserDTO();

            bool IsFound = clsUserRepository.GetAuthenticationUserByUsername(Username, ref User);

            if (!IsFound) return enLoginResult.InvalidCredentials;
            if (Password != User.PasswordHash) return enLoginResult.InvalidCredentials;
            if (User.StatusID != 1) return enLoginResult.InactiveAccount;

            return enLoginResult.Success;
        }

        public static enLoginResult VerifyCredentials(string Username, string Password)
        {
            AuthenticationUserDTO User = new AuthenticationUserDTO();

            bool IsFound = clsUserRepository.GetAuthenticationUserByUsername(Username, ref User);

            if (!IsFound) return enLoginResult.InvalidCredentials;
            if (Password != User.PasswordHash) return enLoginResult.InvalidCredentials;
            if (User.StatusID != 1) return enLoginResult.InactiveAccount;

            return enLoginResult.Success;
        }
    }
}
