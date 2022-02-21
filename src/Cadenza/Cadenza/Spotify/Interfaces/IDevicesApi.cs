namespace Cadenza.Source.Spotify.Player;

public interface IDevicesApi
{
    Task<SpotifyApiDevicesResponse> GetDevices(string accessToken);
}
