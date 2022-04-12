using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Model.Auth;
using Cadenza.Utilities;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify.Api;

internal class AuthApi : IAuthApi
{
    private const string TokenUrl = "https://accounts.spotify.com/api/token";

    private readonly IAuthHelper _authHelper;
    private readonly IHttpHelper _http;

    public AuthApi(IHttpHelper http, IAuthHelper authHelper)
    {
        _http = http;
        _authHelper = authHelper;
    }

    public async Task<CreateSessionResponse> CreateSession(string code, string redirectUri)
    {
        var requestData = new CreateSessionRequest
        {
            code = code,
            redirect_uri = redirectUri,
            grant_type = "authorization_code"
        };

        return await RequestTokens<CreateSessionResponse>(TokenUrl, requestData.AsPostData());
    }

    public async Task<RefreshTokenResponse> RefreshSession(string refreshToken)
    {
        var requestData = new RefreshTokenRequest
        {
            grant_type = "refresh_token",
            refresh_token = refreshToken
        };

        return await RequestTokens<RefreshTokenResponse>(TokenUrl, requestData.AsPostData());
    }

    private async Task<T> RequestTokens<T>(string url, Dictionary<string, string> requestData) where T : class
    {
        var authHeader = await _authHelper.GetAuthHeader();
        var response = await _http.Post(url, authHeader, requestData);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<T>()
            : null;
    }
}
