using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.WinForms.Global
{
    public static class Session
    {
        public static string? AccessToken { get; private set; }
        public static string? RefreshToken { get; private set; }

        public static DateTime AccessTokenExpiration { get; private set; }

        public static UserDetailsDto? User { get; private set; }

        public static void Start(LoginResponseDto loginResponse)
        {
            AccessToken = loginResponse.AccessToken;
            RefreshToken = loginResponse.RefreshToken;
            AccessTokenExpiration = loginResponse.AccessTokenExpiresAt;
        }

        public static void RefreshTokens(LoginResponseDto? response)
        {
            AccessToken = response?.AccessToken;
            RefreshToken = response?.RefreshToken;

            AccessTokenExpiration = response?.AccessTokenExpiresAt ?? default;
        }

        public static void Clear()
        {
            AccessToken = null;
            RefreshToken = null;

            AccessTokenExpiration = default;
        }
    }
}
