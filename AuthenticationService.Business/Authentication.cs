using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public enum AuthenticationResult
    {
        Success = 1,
        InvalidCredentials = 2,
        InactiveAccount = 3,
        LockedAccount = 4,
        DeletedAccount = 5
    }

    public class LoginResult
    {
        public AuthenticationResult Result { get; set; }
        public LoginResponseDto? LoginResponse { get; set; }
    }

    public class Authentication
    {

        private static AuthenticationResult AuthenticateUser(string username,
                                                  string password,
                                                  ref AuthenticationUserDto user)
        {
            bool IsFound = AuthenticationRepository.GetAuthenticationUserByUsername(username, ref user);

            if (!IsFound)
                return AuthenticationResult.InvalidCredentials;

            if (password != user.PasswordHash)
                return AuthenticationResult.InvalidCredentials;

            if (user.StatusID != 1)
                return AuthenticationResult.InactiveAccount;

            return AuthenticationResult.Success;
        }

        public static AuthenticationResult Login(LoginRequestDto request)
        {
            AuthenticationUserDto user = new AuthenticationUserDto();

            return AuthenticateUser(request.Username, request.Password, ref user);
        }

        public static AuthenticationResult VerifyCredentials(LoginRequestDto request)
        {
            AuthenticationUserDto user = new AuthenticationUserDto();

            return AuthenticateUser(request.Username, request.Password, ref user);
        }

        public static AuthenticationResult ChangePassword(ChangePasswordDto request)
        {
            AuthenticationUserDto user = new AuthenticationUserDto();

            AuthenticationResult result = AuthenticateUser(request.Username, request.CurrentPassword, ref user);

            if (result != AuthenticationResult.Success)
                return result;

            bool IsChanged = AuthenticationRepository.ChangePassword(user.ID, request.NewPassword);

            if (!IsChanged)
                return AuthenticationResult.InvalidCredentials;

            return AuthenticationResult.Success;
        }
    }
}
