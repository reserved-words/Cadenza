namespace Cadenza.SyncService.Interfaces;

internal interface IApiTokenFetcher
{
    Task<string> GetToken();
}
