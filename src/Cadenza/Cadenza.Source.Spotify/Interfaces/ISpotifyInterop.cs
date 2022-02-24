namespace Cadenza.Source.Spotify.Interfaces;

public interface ISpotifyInterop
{
    Task<bool> ConnectPlayer(string accessToken);
    Task<bool> DeviceNotFound();
    Task UnexpectedError();
}
