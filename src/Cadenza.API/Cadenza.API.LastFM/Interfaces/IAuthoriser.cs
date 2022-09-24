namespace Cadenza.API.LastFM.Interfaces;

public interface IAuthoriser
{
    Task<string> GetAuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
}
