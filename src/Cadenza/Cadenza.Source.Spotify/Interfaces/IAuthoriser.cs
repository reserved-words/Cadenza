using Cadenza.Source.Spotify.Model;

namespace Cadenza.Source.Spotify.Interfaces;

internal interface IAuthoriser
{
    Task<SpotifyTokens> CreateSession(string code, string redirectUri);
    Task<string> GetAuthHeader();
    Task<string> GetAuthUrl(string state, string redirectUri);
    Task<SpotifyTokens> RefreshSession(string refreshToken);
}
