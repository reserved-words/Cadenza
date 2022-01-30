using Cadenza.Core;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Web;

namespace Cadenza.Source.Spotify;

public class SpotifyStartup : ISpotifyStartup
{
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;
    private readonly IHttpHelper _http;
    private readonly IOptions<PlayerApiConfig> _playerApi;
    private readonly ISpotifyInterop _interop;

    public SpotifyStartup(IStoreGetter storeGetter, IStoreSetter storeSetter, IHttpHelper http, IOptions<PlayerApiConfig> playerApi, ISpotifyInterop interop)
    {
        _storeGetter = storeGetter;
        _storeSetter = storeSetter;
        _http = http;
        _playerApi = playerApi;
        _interop = interop;
    }

    public async Task ConnectToApi(string code, string redirectUri)
    {
        var tokens = await GetSpotifyTokens(code, redirectUri);
        await SaveSpotifyTokens(tokens);
        await RefreshSpotify(tokens.refresh_token);
    }

    public async Task<string> GetAccessToken()
    {
        return await _storeGetter.GetString(StoreKey.SpotifyAccessToken);
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        var apiUrl = GetApiUrl(e => e.SpotifyAuthUrl) + HttpUtility.UrlEncode(redirectUri);
        var response = await _http.Get(apiUrl);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> InitialisePlayer(string accessToken)
    {
        return await _interop.ConnectPlayer(accessToken);
    }

    private async Task<string> GetSpotifyAuthHeader()
    {
        var apiUrl = GetApiUrl(e => e.SpotifyAuthHeader);
        var response = await _http.Get(apiUrl);
        return await response.Content.ReadAsStringAsync();
    }

    private async Task<SpotifyTokens> GetSpotifyTokens(string code, string redirectUri)
    {
        var authHeader = await GetSpotifyAuthHeader();

        var requestData = new Dictionary<string, string>
        {
            { "code", code },
            { "redirect_uri", redirectUri },
            { "grant_type", "authorization_code" }
        };

        var tokenUrl = await GetSpotifyTokenUrl();

        var response = await _http.Post(tokenUrl, requestData, authHeader);

        return await response.Content.ReadFromJsonAsync<SpotifyTokens>();
    }

    private async Task<string> GetSpotifyTokenUrl()
    {
        var apiUrl = GetApiUrl(e => e.SpotifyTokenUrl);
        var response = await _http.Get(apiUrl);
        return await response.Content.ReadAsStringAsync();
    }

    private string GetApiUrl(Func<PlayerApiEndpoints, string> getEndpoint)
    {
        var apiBaseUrl = _playerApi.Value.BaseUrl;
        var apiEndpoint = getEndpoint(_playerApi.Value.Endpoints);
        return $"{apiBaseUrl}{apiEndpoint}";
    }

    private async Task RefreshSpotify(string refreshToken)
    {
        var requestData = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken }
        };

        var tokenUrl = await GetSpotifyTokenUrl();
        var authHeader = await GetSpotifyAuthHeader();

        var response = await _http.Post(tokenUrl, requestData, authHeader);

        var tokens = await response.Content.ReadFromJsonAsync<SpotifyTokens>();
        await SaveSpotifyTokens(tokens);
    }

    private async Task SaveSpotifyTokens(SpotifyTokens tokens)
    {
        await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token);
        await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
    }

    private class SpotifyTokens
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }

    private class TokenRequestData
    {
        public string code { get; set; }
        public string redirect_uri { get; set; }
        public string grant_type { get; set; }
    }
}
