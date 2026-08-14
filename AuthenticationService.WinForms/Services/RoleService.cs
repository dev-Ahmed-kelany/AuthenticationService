using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Dtos.Roles;
using AuthenticationService.WinForms.Global;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AuthenticationService.WinForms.Services
{
    public static class RoleService
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.APIBaseURL)
        };

        public static async Task<Result<int>> AddAsync(SaveRoleDto role, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"Roles");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            var jsonRequest = JsonSerializer.Serialize(role);
            request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

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
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken ?? "" });

                        if (!refreshResult.IsSuccess) return Result<int>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await AddAsync(role, false);
                    }

                    return Result<int>.Failure(
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

                return Result<int>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<int>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<int>.Success(data);
        }

        public static async Task<Result<bool>> UpdateByIDAsync(int id, SaveRoleDto role, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Roles/{id}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            var jsonRequest = JsonSerializer.Serialize(role);
            request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

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
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken ?? "" });

                        if (!refreshResult.IsSuccess) return Result<bool>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await UpdateByIDAsync(id, role, false);
                    }

                    return Result<bool>.Failure(
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

                return Result<bool>.Failure(error);
            }

            return Result<bool>.Success(true);
        }

        public static async Task<Result<List<RoleDetailsDto?>>> SearchAsync(string searchText, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Roles/Search?SearchText={Uri.EscapeDataString(searchText)}");

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

                        if (!refreshResult.IsSuccess) return Result<List<RoleDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await SearchAsync(searchText, false);
                    }

                    return Result<List<RoleDetailsDto?>>.Failure(
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

                return Result<List<RoleDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<RoleDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<RoleDetailsDto?>>.Success(data);
        }

        public static async Task<Result<RoleDetailsDto?>> GetByIDAsync(int id, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Roles/{id}");

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

                        if (!refreshResult.IsSuccess) return Result<RoleDetailsDto?>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetByIDAsync(id, false);
                    }

                    return Result<RoleDetailsDto?>.Failure(
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

                return Result<RoleDetailsDto?>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<RoleDetailsDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<RoleDetailsDto?>.Success(data);
        }

        public static async Task<Result<List<RoleDetailsDto?>>> GetAllAsync(bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Roles");

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

                        if (!refreshResult.IsSuccess) return Result<List<RoleDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetAllAsync(false);
                    }

                    return Result<List<RoleDetailsDto?>>.Failure(
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

                return Result<List<RoleDetailsDto?>>.Failure(error);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<RoleDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<RoleDetailsDto?>>.Success(data);
        }
    }
}