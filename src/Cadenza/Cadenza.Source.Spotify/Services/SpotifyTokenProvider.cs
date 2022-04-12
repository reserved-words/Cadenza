using Cadenza.Core.App;
using Cadenza.Core.Interop;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Model.Auth;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Spotify.Services;

internal class SpotifyTokenProvider : ITokenProvider
{
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;
    private readonly SpotifyApiSettings _settings;
    private readonly IAuthoriser _authoriser;
    private readonly INavigation _navigationInterop;

    public SpotifyTokenProvider(IStoreGetter storeGetter, IStoreSetter storeSetter, IOptions<SpotifyApiSettings> settings, IAuthoriser authoriser, INavigation navigation)
    {
        _storeGetter = storeGetter;
        _storeSetter = storeSetter;
        _settings = settings.Value;
        _authoriser = authoriser;
        _navigationInterop = navigation;
    }

    public async Task<string> GetAccessToken(bool renew)
    {
        await GenerateTokens(renew);
        return await GetValidAccessToken();
    }

    public async Task GenerateTokens(bool renew)
    {
        if (!renew)
        {
            var accessToken = await GetValidAccessToken();
            if (accessToken != null)
                return;
        }

        var refreshToken = await GetValidRefreshToken();
        if (refreshToken != null)
        {
            var isSessionRefreshed = await RefreshSession(refreshToken);
            if (isSessionRefreshed)
                return;
        }

        await CreateSession();
    }

    private async Task<string> GetValidAccessToken()
    {
        return await GetValidStoredToken(StoreKey.SpotifyAccessToken);
    }

    private async Task<string> GetValidRefreshToken()
    {
        return await GetValidStoredToken(StoreKey.SpotifyRefreshToken);
    }

    private async Task<string> GetValidStoredToken(StoreKey key)
    {
        var storedAccessToken = await _storeGetter.GetValue<string>(key);

        if (storedAccessToken == null)
            return null;

        if (storedAccessToken.IsExpired)
            return null;

        return storedAccessToken.Value;
    }

    private async Task<bool> RefreshSession(string refreshToken)
    {
        var tokens = await _authoriser.RefreshSession(refreshToken);
        if (tokens == null)
            return false;

        await PopulateSessionValues(tokens);
        return true;
    }

    private async Task CreateSession()
    {
        await ClearSessionValues();
        var code = await Authorise();
        var tokens = await _authoriser.CreateSession(code, _settings.RedirectUri);
        await PopulateSessionValues(tokens);
    }

    private async Task ClearSessionValues()
    {
        await _storeSetter.Clear(StoreKey.SpotifyAccessToken);
        await _storeSetter.Clear(StoreKey.SpotifyCode);
        await _storeSetter.Clear(StoreKey.SpotifyState);
        await _storeSetter.Clear(StoreKey.SpotifyRefreshToken);
    }

    private async Task PopulateSessionValues(RefreshTokenResponse tokens)
    {
        await _storeSetter.Clear(StoreKey.SpotifyCode);
        await _storeSetter.Clear(StoreKey.SpotifyState);
        await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token, tokens.expires_in);
        await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
    }

    private async Task PopulateSessionValues(CreateSessionResponse tokens)
    {
        await _storeSetter.Clear(StoreKey.SpotifyCode);
        await _storeSetter.Clear(StoreKey.SpotifyState);
        await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token, tokens.expires_in);
        await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
    }

    private async Task<string> Authorise()
    {
        var authUrl = await GetAuthUrl();

        await _navigationInterop.OpenNewTab(authUrl);

        var code = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyCode, 60, CancellationToken.None);

        if (code == null)
            throw new Exception("No code received - need to authenticate on Spotify website");

        return code.Value;
    }

    private async Task<string> GetAuthUrl()
    {
        var guid = Guid.NewGuid();
        var state = guid.ToString().Substring(0, 16);
        await _storeSetter.SetValue(StoreKey.SpotifyState, state);
        return await _authoriser.GetAuthUrl(state, _settings.RedirectUri);
    }
}
