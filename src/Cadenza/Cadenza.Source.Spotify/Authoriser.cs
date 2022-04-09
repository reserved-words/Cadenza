using Cadenza.Core;
using Cadenza.Source.Spotify.Model;
using Cadenza.Source.Spotify.Settings;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify;

public class Authoriser : IAuthoriser
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;
    private readonly SpotifyApiSettings _apiSettings;

    public Authoriser(IUrl url, IHttpHelper http, IOptions<SpotifyApiSettings> apiSettings)
    {
        _url = url;
        _http = http;
        _apiSettings = apiSettings.Value;
    }

    public async Task<SpotifyTokens> CreateSession(string code, string redirectUri)
    {
        var authHeader = await GetAuthHeader();
        var tokenUrl = await GetTokenUrl();

        var requestData = new Dictionary<string, string>
        {
            { "code", code },
            { "redirect_uri", redirectUri },
            { "grant_type", "authorization_code" }
        };

        var response = await _http.Post(tokenUrl, authHeader, requestData);

        return await response.Content.ReadFromJsonAsync<SpotifyTokens>();
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

    public async Task<SpotifyTokens> RefreshSession(string refreshToken)
    {
        var requestData = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken }
        };

        var authHeader = await GetAuthHeader();
        var tokenUrl = await GetTokenUrl();

        var response = await _http.Post(tokenUrl, authHeader, requestData);

        return await response.Content.ReadFromJsonAsync<SpotifyTokens>();
    }

    private async Task<string> GetTokenUrl()
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.TokenUrl);
        return await _http.GetString(url);
    }
}
