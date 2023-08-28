namespace Cadenza.SyncService.Interfaces;

internal interface ISyncHttpHelper
{
    Task Delete<T>(string url, T data);
    Task<T> Get<T>(string url) where T : class, new();
    Task Post<T>(string url, T data);
}
