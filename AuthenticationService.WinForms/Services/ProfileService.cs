using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Dtos.Profile;
using AuthenticationService.WinForms.Global;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AuthenticationService.WinForms.Services
{
    public static class ProfileService
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.APIBaseURL)
        };

        public static async Task<Result<ProfileDetailsDto?>> GetByUserIDAsync(int id, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Profile/{id}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            HttpResponseMessage response = await _Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError =
                    await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResponseError))
                {
                    if (statusCode == 401 && retryAfterRefresh)
                    {
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken });

                        if (!refreshResult.IsSuccess) return Result<ProfileDetailsDto>.Failure(refreshResult.Error);
                        
                        Session.RefreshTokens(refreshResult.Data);

                        return await GetByUserIDAsync(id, false);
                    }

                    return Result<ProfileDetailsDto?>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<ProfileDetailsDto?>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ProfileDetailsDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<ProfileDetailsDto?>.Success(data);
        }
    }
}