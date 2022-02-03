namespace Cadenza.API.Wrapper.Spotify;

public interface IAuthoriser
{
    Task<string> GetAuthHeader();
    Task<string> GetAuthUrl(string redirectUri);
    Task<SpotifyTokens> CreateSession(string code, string redirectUri);
    Task<SpotifyTokens> RefreshSession(string refreshToken);
}