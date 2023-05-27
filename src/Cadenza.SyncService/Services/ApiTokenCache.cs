namespace Cadenza.SyncService.Services;

internal class ApiTokenCache : IApiTokenCache
{
    private string _token;
    private DateTime _expiry;

    public void CacheToken(TokenResponse token)
    {
        _token = token.access_token;
        _expiry = DateTime.Now.AddSeconds(token.expires_in);
    }

    public string GetToken()
    {
        if (_token == null) // No token cached
            return null;

        if (_expiry < DateTime.Now.AddMinutes(5)) // Cached token is about to expire
            return null;

        return _token;
    }
}
