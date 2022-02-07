namespace Cadenza.Source.Spotify.Player;

public interface ISpotifyInterop
{
    Task<bool> ConnectPlayer(string accessToken);
    Task<bool> DeviceNotFound();
    Task UnexpectedError();
}
