namespace Cadenza.API.LastFM.Interfaces;

internal interface IApiClient
{
    Task<T> Get<T>(string url, Func<XElement, T> getValue);
}
