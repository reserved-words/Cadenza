using Cadenza.Core;
using Cadenza.Source.Spotify;

namespace Cadenza;

public class SpotifyConfig : ISpotifyApiConfig
{
    private readonly IStoreGetter _store;

    public SpotifyConfig(IStoreGetter store)
    {
        _store = store;
    }

    public async Task<string> AccessToken()
    {
        return (await _store.GetValue<string>(StoreKey.SpotifyAccessToken)).Value;
    }

    public async Task<string> DeviceId()
    {
        return (await _store.GetValue<string>(StoreKey.SpotifyDeviceId)).Value;
    }
}