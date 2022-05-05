namespace Cadenza.Spotify.Interfaces;

public interface IAuthoriser
{
    Task<string> GetAuthHeader();
    Task<string> GetAuthUrl(string state, string redirectUri);
}
