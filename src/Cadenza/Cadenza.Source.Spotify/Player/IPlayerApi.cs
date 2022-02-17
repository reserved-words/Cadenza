namespace Cadenza.Source.Spotify.Player;

public interface IPlayerApi
{
    Task<SpotifyApiDevicesResponse> GetDevices();
    Task<SpotifyApiPlayState> GetPlayState();
    Task Play(string trackId = null);
    Task Pause();
}