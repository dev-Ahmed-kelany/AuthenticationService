using AuthenticationService.Dtos.Authentication;
using AuthenticationService.WinForms.Global;
using System.Text.Json;
using System.Text;

namespace AuthenticationService.WinForms.Services
{
    //public static class AuthenticationServiceErrors

    public static class AuthenticationService
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.APIBaseURL)
        };

        public static async Task<Result<LoginResponseDto>> LoginAsync(AuthenticationRequestDto request)
        {
            var jsonRequest = JsonSerializer.Serialize(request);

            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _Client.PostAsync("Auth/Login", content);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError = await response.Content.ReadAsStringAsync();

                var apiError = JsonSerializer.Deserialize<ApiError>(jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(apiError?.Code ?? "Unknown", apiError?.Message ?? "An unexpected error occured", statusCode);

                return Result<LoginResponseDto>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<LoginResponseDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<LoginResponseDto>.Success(data);
        }

        public static async Task<Result<LoginResponseDto>> RefreshAsync(RefreshTokenRequestDto request)
        {
            var jsonRequest = JsonSerializer.Serialize(request);

            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _Client.PostAsync("Auth/Refresh", content);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError = await response.Content.ReadAsStringAsync();

                var apiError = JsonSerializer.Deserialize<ApiError>(jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(apiError?.Code ?? "Unknown", apiError?.Message ?? "An unexpected error occured", statusCode);

                return Result<LoginResponseDto>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<LoginResponseDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<LoginResponseDto>.Success(data);
        }

        public static async Task<Result<bool>> VerifyCredentialsAsync(AuthenticationRequestDto request)
        {
            var jsonRequest = JsonSerializer.Serialize(request);

            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _Client.PostAsync("Auth/verify-credentials", content);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError = await response.Content.ReadAsStringAsync();

                var apiError = JsonSerializer.Deserialize<ApiError>(jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(apiError?.Code ?? "Unknown", apiError?.Message ?? "An unexpected error occured", statusCode);

                return Result<bool>.Failure(error);
            }

            return Result<bool>.Success(true);
        }

        public static async Task<Result<bool>> ChangePasswordAsync(ChangePasswordDto request)
        {
            var jsonRequest = JsonSerializer.Serialize(request);

            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _Client.PostAsync("Auth/change-password", content);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError = await response.Content.ReadAsStringAsync();

                var apiError = JsonSerializer.Deserialize<ApiError>(jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(apiError?.Code ?? "Unknown", apiError?.Message ?? "An unexpected error occured", statusCode);

                return Result<bool>.Failure(error);
            }

            return Result<bool>.Success(true);
        }

        public static async Task<Result<bool>> LogoutAsync(RefreshTokenRequestDto request)
        {
            var jsonRequest = JsonSerializer.Serialize(request);

            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _Client.PostAsync("Auth/logout", content);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError = await response.Content.ReadAsStringAsync();

                var apiError = JsonSerializer.Deserialize<ApiError>(jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(apiError?.Code ?? "Unknown", apiError?.Message ?? "An unexpected error occured", statusCode);

                return Result<bool>.Failure(error);
            }

            return Result<bool>.Success(true);
        }
    }
}