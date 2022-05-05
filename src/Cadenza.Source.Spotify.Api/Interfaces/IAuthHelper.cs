namespace Cadenza.Source.Spotify.Api.Interfaces;

public interface IAuthHelper
{
    Task<string> GetAuthHeader();
    Task<string> GetAuthUrl(string state, string redirectUri);
}