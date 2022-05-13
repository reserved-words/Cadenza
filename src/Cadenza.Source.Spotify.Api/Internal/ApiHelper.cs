using Cadenza.Source.Spotify.Api.Exceptions;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Responses;
using Cadenza.Utilities;
using System.Net;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify.Api.Internal;

internal class ApiHelper : IApiHelper
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IHttpHelper _httpClient;

    public ApiHelper(IHttpHelper httpClient, ITokenProvider tokenProvider)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
    }

    public async Task<ApiResponse<T>> Get<T>(string url) where T : class
    {
        var response = await TryCall((bool renewToken) => AuthorisedGet(renewToken, url));

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

    public async Task<ApiResponse> Put(string url, object data = null)
    {
        var response = await TryCall((bool renewToken) => AuthorisedPut(renewToken, url, data));

        var error = !response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ApiError>()
            : null;

        return new ApiResponse(error?.Error);
    }

    public async Task<ApiResponse> Delete(string url, object data = null)
    {
        var response = await TryCall((bool renewToken) => AuthorisedDelete(renewToken, url, data));

        var error = !response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ApiError>()
            : null;

        return new ApiResponse(error?.Error);
    }

    private async Task<HttpResponseMessage> AuthorisedGet(bool renewToken, string url)
    {
        var authHeader = await GetAuthHeader(renewToken);
        var response = await _httpClient.Get(url, authHeader);
        return response;
    }

    private async Task<HttpResponseMessage> AuthorisedDelete(bool renewToken, string url, object data = null)
    {
        var authHeader = await GetAuthHeader(renewToken);
        return await _httpClient.Delete(url, authHeader, data);
    }

    private async Task<HttpResponseMessage> AuthorisedPut(bool renewToken, string url, object data = null)
    {
        var authHeader = await GetAuthHeader(renewToken);
        return await _httpClient.Put(url, authHeader, data);
    }

    private static void CheckForCommonErrors(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizedException();

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new NotFoundException();
    }

    private async Task<string> GetAuthHeader(bool renewToken)
    {
        var accessToken = await _tokenProvider.GetAccessToken(renewToken);
        return $"Bearer {accessToken}";
    }

    private async Task<HttpResponseMessage> TryCall(Func<bool, Task<HttpResponseMessage>> apiCall)
    {
        var response = await apiCall(false);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            response = await apiCall(true);
        }

        CheckForCommonErrors(response);

        return response;
    }
}

