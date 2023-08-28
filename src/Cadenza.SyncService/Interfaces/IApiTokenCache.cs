namespace Cadenza.SyncService.Interfaces;

internal interface IApiTokenCache
{
    string GetToken();
    void CacheToken(TokenResponse tokenResponse);
}