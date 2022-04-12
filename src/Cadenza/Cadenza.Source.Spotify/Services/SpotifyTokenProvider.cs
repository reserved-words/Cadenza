using Cadenza.Core.Interop;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Spotify.Services;

internal class SpotifyTokenProvider : ITokenProvider
{
    private readonly ISpotifySession _session;
    private readonly SpotifyApiSettings _settings;
    private readonly IAuthApi _authApi;
    private readonly IAuthHelper _authHelper;
    private readonly INavigation _navigationInterop;

    public SpotifyTokenProvider(ISpotifySession session, IOptions<SpotifyApiSettings> settings, IAuthApi authApi, 
        INavigation navigation, IAuthHelper authHelper)
    {
        _session = session;
        _settings = settings.Value;
        _authApi = authApi;
        _navigationInterop = navigation;
        _authHelper = authHelper;
    }

    public async Task<string> GetAccessToken(bool renew)
    {
        await GenerateTokens(renew);
        return await _session.GetValidAccessToken();
    }

    public async Task GenerateTokens(bool renew)
    {
        if (!renew)
        {
            var accessToken = await _session.GetValidAccessToken();
            if (accessToken != null)
                return;
        }

        var refreshToken = await _session.GetValidRefreshToken();
        if (refreshToken != null)
        {
            var isSessionRefreshed = await RefreshSession(refreshToken);
            if (isSessionRefreshed)
                return;
        }

        await CreateSession();
    }

    private async Task<bool> RefreshSession(string refreshToken)
    {
        var tokens = await _authApi.RefreshSession(refreshToken);
        if (tokens == null)
            return false;

        await _session.Populate(tokens);
        return true;
    }

    private async Task CreateSession()
    {
        await _session.Clear();
        var code = await Authorise();
        var tokens = await _authApi.CreateSession(code, _settings.RedirectUri);
        await _session.Populate(tokens);
    }

    private async Task<string> Authorise()
    {
        var authUrl = await GetAuthUrl();

        await _navigationInterop.OpenNewTab(authUrl);

        var code = await _session.AwaitCode();

        if (code == null)
            throw new Exception("No code received - need to authenticate on Spotify website");

        return code;
    }

    private async Task<string> GetAuthUrl()
    {
        var state = await _session.SetState();
        return await _authHelper.GetAuthUrl(state, _settings.RedirectUri);
    }
}
