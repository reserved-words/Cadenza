using Cadenza.Spotify.API.Interfaces;
using Cadenza.Utilities;
using System.Net;
using System.Net.Http.Json;

namespace Cadenza.Spotify.API;

internal class ApiHelper : IApiHelper
{
    private readonly IApiToken _apiToken;
    private readonly IHttpHelper _httpClient;

    public ApiHelper(IHttpHelper httpClient, IApiToken apiToken)
    {
        _httpClient = httpClient;
        _apiToken = apiToken;
    }

    public async Task<ApiResponse<T>> Get<T>(string url) where T : class
    {
        var accessToken = _apiToken.GetAccessToken();
        var authHeader = $"Bearer {accessToken}";
        var response = await _httpClient.Get(url, authHeader);

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
    public ApiResponse(T data = default(T))
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
