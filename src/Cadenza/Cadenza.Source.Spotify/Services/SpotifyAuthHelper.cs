using Cadenza.Core.App;
using Cadenza.Core.Interop;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Spotify.Services;

internal class SpotifyAuthHelper : ISpotifyAuthHelper
{
    // Add to config
    private const int AccessTokenExpiryMinutes = 120;
    private const int AccessTokenRefreshMinutes = 60;

    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;
    private readonly SpotifyApiSettings _settings;
    private readonly IAuthoriser _authoriser;
    private readonly INavigation _navigationInterop;
    private readonly IMessenger _messenger;

    public SpotifyAuthHelper(IStoreGetter storeGetter, IStoreSetter storeSetter, IOptions<SpotifyApiSettings> settings,
         IAuthoriser lastFmAuthoriser, INavigation navigationInterop, IMessenger messenger)
    {
        _storeGetter = storeGetter;
        _storeSetter = storeSetter;
        _settings = settings.Value;
        _authoriser = lastFmAuthoriser;
        _navigationInterop = navigationInterop;
        _messenger = messenger;
    }

    public async Task<string> GetAccessToken(CancellationToken cancellationToken)
    {
        var storedAccessToken = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);

        if (storedAccessToken == null || storedAccessToken.Updated.AddMinutes(AccessTokenExpiryMinutes) <= DateTime.Now)
        {
            return await CreateSession(CancellationToken.None);
        }
        else if (storedAccessToken.Updated.AddMinutes(AccessTokenRefreshMinutes) <= DateTime.Now)
        {
            return await RefreshSession();
        }

        return storedAccessToken.Value;
    }

    public async Task<string> CreateSession(CancellationToken cancellationToken)
    {
        await _storeSetter.Clear(StoreKey.SpotifyAccessToken);
        await _storeSetter.Clear(StoreKey.SpotifyCode);
        await _storeSetter.Clear(StoreKey.SpotifyState);
        await _storeSetter.Clear(StoreKey.SpotifyRefreshToken);

        var authUrl = await GetAuthUrl();
        var code = await Authorise(authUrl, cancellationToken);
        var tokens = await _authoriser.CreateSession(code, _settings.RedirectUri);

        await _storeSetter.Clear(StoreKey.SpotifyCode);
        await _storeSetter.Clear(StoreKey.SpotifyState);
        await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token);
        await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
        return tokens.access_token;
    }

    private async Task<string> RefreshSession()
    {
        var storedRefreshToken = await _storeGetter.GetValue<string>(StoreKey.SpotifyRefreshToken);
        var tokens = await _authoriser.RefreshSession(storedRefreshToken.Value);
        await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token);
        await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
        return tokens.access_token;
    }

    private async Task<string> GetAuthUrl()
    {
        var guid = Guid.NewGuid();
        var state = guid.ToString().Substring(0, 16);
        await _storeSetter.SetValue(StoreKey.SpotifyState, state);
        return await _authoriser.GetAuthUrl(state, _settings.RedirectUri);
    }

    private async Task<string> Authorise(string authUrl, CancellationToken cancellationToken)
    {
var code = await _navigationInterop.OpenNewTabAndAwaitValue<string>(authUrl, StoreKey.SpotifyCode, cancellationToken);

        if (code == null)
            throw new Exception("No code received - need to authenticate on Spotify website");

        return code.Value;
    }
}
