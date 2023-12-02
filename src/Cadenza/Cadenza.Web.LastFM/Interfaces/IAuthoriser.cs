namespace Cadenza.Web.LastFM.Interfaces;

public interface IAuthoriser
{
    Task<string> GetAuthUrl(string redirectUri);
    Task CreateSession(string token);
    Task<bool> DoesSessionExist();
}
