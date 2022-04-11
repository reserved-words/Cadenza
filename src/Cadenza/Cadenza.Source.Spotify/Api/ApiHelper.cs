using Cadenza.Core.App;
using Cadenza.Source.Spotify.Exceptions;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Utilities;
using System.Net;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify.Api;

internal class ApiHelper : IApiHelper
{
    private readonly IStoreGetter _store;
    private readonly IHttpHelper _httpClient;

    public ApiHelper(IHttpHelper httpClient, IStoreGetter store)
    {
        _httpClient = httpClient;
        _store = store;
    }

    public async Task<ApiResponse<T>> Get<T>(string url) where T : class
    {
        var authHeader = await GetAuthHeader();
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

    public async Task<ApiResponse> Post(string url, object data = null)
    {
        var authHeader = await GetAuthHeader();

        var response = await _httpClient.Post(url, authHeader, data);

        CheckForCommonErrors(response);

        var error = !response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ApiError>()
            : null;

        return new ApiResponse(error?.Error);
    }

    public async Task<ApiResponse> Put(string url, object data = null)
    {
        var accessToken = await GetAccessToken();
        return await Put(url, accessToken, data);
    }

    public async Task<ApiResponse> Put(string url, string accessToken, object data = null)
    {
        var authHeader = await GetAuthHeader(accessToken);

        var response = await _httpClient.Put(url, authHeader, data);

        CheckForCommonErrors(response);

        var error = !response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ApiError>()
            : null;

        return new ApiResponse(error?.Error);
    }

    public async Task<ApiResponse<T>> Get<T>(string url, string accessToken) where T : class
    {
        var authHeader = await GetAuthHeader(accessToken);
        var response = await _httpClient.Get(url, authHeader);

        CheckForCommonErrors(response);

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

    private void CheckForCommonErrors(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizedApiException();

        // actually in some cases this could mean something else - narrow down more
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new DeviceNotFoundException();
    }

    private async Task<string> GetAuthHeader(string accessToken = null)
    {
        accessToken ??= await GetAccessToken();
        return $"Bearer {accessToken}";
    }

    private async Task<string> GetAccessToken()
    {
        return (await _store.GetValue<string>(StoreKey.SpotifyAccessToken)).Value;
    }
}

