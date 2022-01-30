namespace Cadenza.API.Core.Spotify;

public interface IAuthoriser
{
    Task<string> GetAuthHeader();
    Task<string> GetTokenUrl();
    Task<string> GetAuthUrl(string redirectUri);
}