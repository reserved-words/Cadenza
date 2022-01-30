namespace Cadenza.LastFM;

public interface ILastFmStartup
{
    Task<string> GetAuthUrl(string redirectUri);
    Task<string> GetSessionKey();
    Task<bool> CreateSession(string token);
}
