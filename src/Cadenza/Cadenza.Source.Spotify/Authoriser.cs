using Cadenza.Core;
using Cadenza.Source.Spotify.Api.Model.Auth;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Settings;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify;

public class Authoriser : IAuthoriser
{
    private const string TokenUrl = "https://accounts.spotify.com/api/token";

    private readonly IUrl _url;
    private readonly IHttpHelper _http;
    private readonly SpotifyApiSettings _apiSettings;

    public Authoriser(IUrl url, IHttpHelper http, IOptions<SpotifyApiSettings> apiSettings)
    {
        _url = url;
        _http = http;
        _apiSettings = apiSettings.Value;
    }

    public async Task<CreateSessionResponse> CreateSession(string code, string redirectUri)
    {
        var authHeader = await GetAuthHeader();

        var requestData = new CreateSessionRequest
        {
            code = code,
            redirect_uri = redirectUri,
            grant_type = "authorization_code"
        };

        var response = await _http.Post(TokenUrl, authHeader, requestData.AsPostData());

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<CreateSessionResponse>()
            : null;
    }

    public async Task<string> GetAuthHeader()
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AuthHeader);
        return await _http.GetString(url);
    }

    public async Task<string> GetAuthUrl(string state, string redirectUri)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.AuthUrl, ("state", state), ("redirectUri", redirectUri));
        return await _http.GetString(url);
    }

    public async Task<RefreshTokenResponse> RefreshSession(string refreshToken)
    {
        var requestData = new RefreshTokenRequest
        {
            grant_type = "refresh_token",
            refresh_token = refreshToken
        };

        var authHeader = await GetAuthHeader();

        var response = await _http.Post(TokenUrl, authHeader, requestData.AsPostData());

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<RefreshTokenResponse>()
            : null;
    }
}
