namespace Cadenza.Source.Spotify;

public interface ISpotifyApiConfig
{
    Task<string> DeviceId();
    Task<string> AccessToken();
}