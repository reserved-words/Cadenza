using Cadenza.Source.Spotify.Api.Model.Auth;

namespace Cadenza.Source.Spotify.Api.Interfaces;

public interface IAuthApi
{
    Task<CreateSessionResponse> CreateSession(string code, string redirectUri);
    Task<RefreshTokenResponse> RefreshSession(string refreshToken);
}
