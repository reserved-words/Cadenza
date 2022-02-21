using Cadenza.Utilities;
using System.Net;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify.Player;

public class ApiHelper : IApiHelper
{
    private readonly IHttpHelper _httpClient;

    public ApiHelper(IHttpHelper httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse> Put(string url, string accessToken, object data = null)
    {
        var authHeader = GetAuthHeader(accessToken);

        var response = await _httpClient.Put(url, authHeader, data);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizedApiException();
        
        var error = !response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ApiError>()
            : null;

        return new ApiResponse(error?.Error);
    }

    public async Task<ApiResponse<T>> Get<T>(string url, string accessToken) where T : class
    {
        var authHeader = GetAuthHeader(accessToken);
        var response = await _httpClient.Get(url, authHeader);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizedApiException();

        if (response.IsSuccessStatusCode)
        {
            var data = response.StatusCode == HttpStatusCode.NoContent
                ? null
                : await response.Content.ReadFromJsonAsync<T>();

            return new ApiResponse<T>(data);
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            return new ApiResponse<T>(error);
        }
    }

    private static string GetAuthHeader(string accessToken)
    {
        return $"Bearer {accessToken}";
    }
}
