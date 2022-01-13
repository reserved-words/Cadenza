namespace Cadenza.Source.Spotify;

public interface ISpotifyPlayerApi
{
    Task<SpotifyApiPlayState> GetPlayState();
    Task Play(string trackId = null);
    Task Pause();
}