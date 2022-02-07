namespace Cadenza.Source.Spotify.Player;

public interface ISpotifyApiConfig
{
    Task<string> DeviceId();
    Task<string> AccessToken();
}