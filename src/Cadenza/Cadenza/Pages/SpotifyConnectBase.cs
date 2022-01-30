using Cadenza.Common;
using Cadenza.Source.Spotify;
using Cadenza.Utilities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza;

public class SpotifyConnectBase : ComponentBase
{
    [Inject]
    public IStoreGetter StoreGetter { get; set; }

    [Inject]
    public IStoreSetter StoreSetter { get; set; }

    [Inject]
    public IHttpHelper Http { get; set; }

    [Inject]
    public IOptions<PlayerApiConfig> PlayerApi { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAppController App { get; set; }

    [Inject]
    public ISpotifyStartup StartupService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (QueryHelpers.ParseQuery(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).Query).TryGetValue("code", out var code))
        {
            var tokens = await GetSpotifyTokens(code);
            await SaveSpotifyTokens(tokens);
            await RefreshSpotify(tokens.refresh_token);
            NavigationManager.NavigateTo("/");
            return;
        }

        var accessToken = await StoreGetter.GetString(StoreKey.SpotifyAccessToken);
        if (accessToken != null)
        {
            var connected = await StartupService.ConnectPlayer(accessToken);
            // Change to get Device ID here

            if (connected)
            {
                await App.EnableSource(LibrarySource.Spotify);
            }
            else
            {
                await App.DisableSource(new SourceException(LibrarySource.Spotify, SourceError.ConnectFailure, "Failed to connect"));
            }
            return;
        }

        var authUrl = await GetSpotifyAuthUrl();

        NavigationManager.NavigateTo(authUrl);
    }

    private async Task SaveSpotifyTokens(SpotifyTokens tokens)
    {
        await StoreSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token);
        await StoreSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
    }

    private async Task<string> GetSpotifyAuthHeader()
    {
        var apiBaseUrl = PlayerApi.Value.BaseUrl;
        var apiEndpoint = PlayerApi.Value.Endpoints.SpotifyAuthHeader;
        var apiUrl = $"{apiBaseUrl}{apiEndpoint}";
        var response = await Http.Get(apiUrl);
        return await response.Content.ReadAsStringAsync();
    }

    private async Task<string> GetSpotifyAuthUrl()
    {
        var apiBaseUrl = PlayerApi.Value.BaseUrl;
        var apiEndpoint = PlayerApi.Value.Endpoints.SpotifyAuthUrl;
        var redirectUri = "http://localhost:29085/SpotifyConnect";  //todo
        var apiUrl = $"{apiBaseUrl}{apiEndpoint}{HttpUtility.UrlEncode(redirectUri)}";
        var response = await Http.Get(apiUrl);
        return await response.Content.ReadAsStringAsync();
    }

    private async Task<SpotifyTokens> GetSpotifyTokens(string code)
    {
        var authHeader = await GetSpotifyAuthHeader();

        var requestData = new Dictionary<string, string>
        {
            { "code", code },
            { "redirect_uri", "http://localhost:29085/SpotifyConnect" }, //todo
            { "grant_type", "authorization_code" }
        };

        var tokenUrl = await GetSpotifyTokenUrl();

        var response = await Http.Post(tokenUrl, requestData, authHeader);

        return await response.Content.ReadFromJsonAsync<SpotifyTokens>();
    }

    public class TokenRequestData
    {
        public string code { get; set; }
        public string redirect_uri { get; set; }
        public string grant_type { get; set; }
    }

    private async Task<string> GetSpotifyTokenUrl()
    {
        var apiBaseUrl = PlayerApi.Value.BaseUrl;
        var apiEndpoint = PlayerApi.Value.Endpoints.SpotifyTokenUrl;
        var apiUrl = $"{apiBaseUrl}{apiEndpoint}";
        var response = await Http.Get(apiUrl);
        return await response.Content.ReadAsStringAsync();
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

        var response = await Http.Post(tokenUrl, requestData, authHeader);

        if (response.IsSuccessStatusCode)
        {
            var tokens = await response.Content.ReadFromJsonAsync<SpotifyTokens>();
            await SaveSpotifyTokens(tokens);
        }
        else
        {
            var test = await response.Content.ReadAsStringAsync();
            throw new Exception();
        }
    }

    private class SpotifyTokens
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}