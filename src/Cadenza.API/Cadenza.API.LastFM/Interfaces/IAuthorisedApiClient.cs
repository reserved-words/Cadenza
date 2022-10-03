namespace Cadenza.API.LastFM.Interfaces;

internal interface IAuthorisedApiClient
{
    Task Post(string sessionKey, Dictionary<string, string> parameters);
}