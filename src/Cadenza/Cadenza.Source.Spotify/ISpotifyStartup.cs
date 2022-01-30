namespace Cadenza.Source.Spotify;

public interface ISpotifyStartup
{
    Task ConnectToApi(string code, string redirectUri);
    Task<bool> InitialisePlayer(string accessToken);
    Task<string> GetAuthUrl(string redirectUri);
    Task<string> GetAccessToken();
}