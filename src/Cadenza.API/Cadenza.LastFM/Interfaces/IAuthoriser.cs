namespace Cadenza.LastFM;

public interface IAuthoriser
{
    Task<string> GetAuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
}
