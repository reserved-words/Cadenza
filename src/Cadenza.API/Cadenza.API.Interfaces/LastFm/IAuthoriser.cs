namespace Cadenza.API.Interfaces.LastFm;

public interface IAuthoriser
{
    Task<string> GetAuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
}
