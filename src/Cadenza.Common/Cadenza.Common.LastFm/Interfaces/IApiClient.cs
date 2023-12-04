namespace Cadenza.Common.LastFm.Interfaces;

internal interface IApiClient
{
    Task<T> Get<T>(Dictionary<string, string> parameters, Func<XElement, T> getValue);
}
