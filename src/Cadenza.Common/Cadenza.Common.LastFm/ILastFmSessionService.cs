namespace Cadenza.API.Interfaces.LastFm;

public interface ILastFmSessionService
{
    Task<string> GetAuthUrl(string redirectUri);
    Task<(string Username, string SessionKey)> CreateSession(string token);
}
