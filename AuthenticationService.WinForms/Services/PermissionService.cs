using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Dtos.Permissions;
using AuthenticationService.WinForms.Global;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AuthenticationService.WinForms.Services
{
    public static class PermissionService
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.APIBaseURL)
        };

        public static async Task<Result<int>> AddAsync(CreatePermissionDto permission, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"Permissions");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            var jsonRequest = JsonSerializer.Serialize(permission);
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

                        return await AddAsync(permission, false);
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

        public static async Task<Result<bool>> UpdateByIDAsync(int id, UpdatePermissionDto permission, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Permissions/{id}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            var jsonRequest = JsonSerializer.Serialize(permission);
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

                        return await UpdateByIDAsync(id, permission, false);
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

        public static async Task<Result<List<PermissionDetailsDto?>>> SearchAsync(string searchText, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Permissions/Search?SearchText={Uri.EscapeDataString(searchText)}");

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

                        if (!refreshResult.IsSuccess) return Result<List<PermissionDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await SearchAsync(searchText, false);
                    }

                    return Result<List<PermissionDetailsDto?>>.Failure(
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

                return Result<List<PermissionDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<PermissionDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<PermissionDetailsDto?>>.Success(data);
        
        }

        public static async Task<Result<PermissionDetailsDto?>> GetByIDAsync(int id, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Permissions/{id}");

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

                        if (!refreshResult.IsSuccess) return Result<PermissionDetailsDto?>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetByIDAsync(id, false);
                    }

                    return Result<PermissionDetailsDto?>.Failure(
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

                return Result<PermissionDetailsDto?>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<PermissionDetailsDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<PermissionDetailsDto?>.Success(data);
        }

        public static async Task<Result<List<PermissionDetailsDto?>>> GetAllAsync(bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Permissions");

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

                        if (!refreshResult.IsSuccess) return Result<List<PermissionDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetAllAsync(false);
                    }

                    return Result<List<PermissionDetailsDto?>>.Failure(
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

                return Result<List<PermissionDetailsDto?>>.Failure(error);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<PermissionDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<PermissionDetailsDto?>>.Success(data);
        }
    }
}