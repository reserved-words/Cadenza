namespace Cadenza.LastFM;

public interface ILastFmAuthorisedClient 
{
    Task Post(string sessionKey, Dictionary<string, string> parameters);
}