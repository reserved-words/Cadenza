namespace Cadenza.Source.Spotify;

public interface ISpotifyInterop
{
    Task<bool> ConnectPlayer(string accessToken);
    Task<bool> DeviceNotFound();
    Task UnexpectedError();
}
