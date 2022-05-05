using Cadenza.Source.Spotify.Api.Model.Auth;

namespace Cadenza.Source.Spotify.Interfaces;

internal interface ISpotifySession
{
    Task Clear();
    Task<string> GetValidAccessToken();
    Task<string> GetValidRefreshToken();
    Task Populate(RefreshTokenResponse tokens);
    Task Populate(CreateSessionResponse tokens);
    Task<string> SetState();
    Task<string> AwaitCode();
}