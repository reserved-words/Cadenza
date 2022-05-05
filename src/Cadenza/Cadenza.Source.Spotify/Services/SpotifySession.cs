using Cadenza.Core.App;
using Cadenza.Source.Spotify.Api.Model.Auth;
using Cadenza.Source.Spotify.Interfaces;

namespace Cadenza.Source.Spotify.Services;

internal class SpotifySession : ISpotifySession
{
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;

    public SpotifySession(IStoreSetter storeSetter, IStoreGetter storeGetter)
    {
        _storeSetter = storeSetter;
        _storeGetter = storeGetter;
    }

    public async Task Clear()
    {
        await _storeSetter.Clear(StoreKey.SpotifyAccessToken);
        await _storeSetter.Clear(StoreKey.SpotifyCode);
        await _storeSetter.Clear(StoreKey.SpotifyState);
        await _storeSetter.Clear(StoreKey.SpotifyRefreshToken);
    }

    public async Task Populate(RefreshTokenResponse tokens)
    {
        await _storeSetter.Clear(StoreKey.SpotifyCode);
        await _storeSetter.Clear(StoreKey.SpotifyState);
        await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token, tokens.expires_in);
        await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
    }

    public async Task Populate(CreateSessionResponse tokens)
    {
        await _storeSetter.Clear(StoreKey.SpotifyCode);
        await _storeSetter.Clear(StoreKey.SpotifyState);
        await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token, tokens.expires_in);
        await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
    }

    public async Task<string> SetState()
    {
        var guid = Guid.NewGuid();
        var state = guid.ToString().Substring(0, 16);
        await _storeSetter.SetValue(StoreKey.SpotifyState, state);
        return state;
    }

    public async Task<string> GetValidAccessToken()
    {
        return await GetValidStoredToken(StoreKey.SpotifyAccessToken);
    }

    public async Task<string> GetValidRefreshToken()
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

    public async Task<string> AwaitCode()
    {
        var storedCode = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyCode, 60, CancellationToken.None);
        return storedCode?.Value;
    }
}
