using Cadenza.API.Core.LastFM;
using Cadenza.Core;

namespace Cadenza.LastFM;

public class LastFmStartup : ILastFmStartup
{
    private readonly IAuthoriser _authoriser;
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;

    public LastFmStartup(IStoreGetter storeGetter, IStoreSetter storeSetter, IAuthoriser authoriser)
    {
        _storeGetter = storeGetter;
        _storeSetter = storeSetter;
        _authoriser = authoriser;
    }

    public async Task<bool> CreateSession(string token)
    {
        var sessionKey = await _authoriser.CreateSession(token);
        await SaveSessionKey(sessionKey);

        if (!string.IsNullOrEmpty(sessionKey))
        {
            return true;
        }

        return false;
    }

    public async Task<string> GetAuthUrl(string redirectUri)
    {
        return await _authoriser.GetAuthUrl(redirectUri);
    }

    public async Task<string> GetSessionKey()
    {
        return await _storeGetter.GetString(StoreKey.LastFmSessionKey);
    }

    private async Task SaveSessionKey(string sessionKey)
    {
        await _storeSetter.SetValue(StoreKey.LastFmSessionKey, sessionKey);
    }
}