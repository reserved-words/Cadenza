namespace Cadenza.Common.LastFm.Interfaces;

internal interface IAuthorisedApiClient
{
    Task Post(string sessionKey, Dictionary<string, string> parameters);
}