using Cadenza.API.Core.Spotify;
using Cadenza.Core;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify;

public class SpotifyStartup : ISpotifyStartup
{
    private readonly IAuthoriser _authoriser;
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;
    private readonly IHttpHelper _http;
    private readonly ISpotifyInterop _interop;

    public SpotifyStartup(IStoreGetter storeGetter, IStoreSetter storeSetter, IHttpHelper http, ISpotifyInterop interop, IAuthoriser authoriser)
    {
        _storeGetter = storeGetter;
        _storeSetter = storeSetter;
        _http = http;
        _interop = interop;
        _authoriser = authoriser;
    }

    public async Task StartSession(string code, string redirectUri)
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
        return await _authoriser.GetAuthUrl(redirectUri);
    }

    public async Task<bool> InitialisePlayer(string accessToken)
    {
        return await _interop.ConnectPlayer(accessToken);
    }

    private async Task<SpotifyTokens> GetSpotifyTokens(string code, string redirectUri)
    {
        var authHeader = await _authoriser.GetAuthHeader();

        var requestData = new Dictionary<string, string>
        {
            { "code", code },
            { "redirect_uri", redirectUri },
            { "grant_type", "authorization_code" }
        };

        var tokenUrl = await _authoriser.GetTokenUrl();

        var response = await _http.Post(tokenUrl, requestData, authHeader);

        return await response.Content.ReadFromJsonAsync<SpotifyTokens>();
    }

    private async Task RefreshSpotify(string refreshToken)
    {
        var requestData = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken }
        };

        var tokenUrl = await _authoriser.GetTokenUrl();
        var authHeader = await _authoriser.GetAuthHeader();

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
