using Cadenza.Utilities;
using System.Net;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify.Player;

public class ApiHelper : IApiHelper
{
    private readonly ISpotifyApiConfig _config;
    private readonly IHttpHelper _httpClient;

    public ApiHelper(IHttpHelper httpClient, ISpotifyApiConfig config)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<ApiResponse> Put(string urlFormat, object data = null)
    {
        var deviceId = await _config.DeviceId();
        var url = string.Format(urlFormat, deviceId);
        var authHeader = await GetAuthHeader();
        var response = await _httpClient.Put(url, authHeader, data);

        var error = !response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ApiError>()
            : null;

        return new ApiResponse(error?.Error);
    }

    public async Task<ApiResponse<T>> Get<T>(string url) where T : class
    {
        var authHeader = await GetAuthHeader();
        var response = await _httpClient.Get(url, authHeader);

        var test = await response.Content.ReadAsStringAsync();

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

    private async Task<string> GetAuthHeader()
    {
        var accessToken = await _config.AccessToken();
        var authHeader = $"Bearer {accessToken}";
        return authHeader;
    }
}

public class ApiErrorDetails
{
    public int Status { get; set; }
    public string Message { get; set; }
}

public class ApiError
{
    public ApiErrorDetails Error { get; set; }
}

public class ApiResponse
{
    public ApiResponse(ApiErrorDetails error = null)
    {
        Error = error;
    }

    public bool Success => Error == null;
    public ApiErrorDetails Error { get; }
}

public class ApiResponse<T>
{
    public ApiResponse(T data = default)
    {
        Data = data;
    }

    public ApiResponse(ApiError error)
    {
        Error = error;
    }

    public bool Success => Error == null;
    public T Data { get; }
    public ApiError Error { get; }
}
