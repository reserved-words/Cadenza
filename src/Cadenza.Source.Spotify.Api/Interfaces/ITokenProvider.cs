namespace Cadenza.Source.Spotify.Api.Interfaces;

public interface ITokenProvider
{
    Task<string> GetAccessToken(bool renew);
    Task GenerateTokens(bool renew);
}