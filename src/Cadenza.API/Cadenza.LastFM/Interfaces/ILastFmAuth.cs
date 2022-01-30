namespace Cadenza.LastFM;

public interface ILastFmAuth
{
    string GetAuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
}