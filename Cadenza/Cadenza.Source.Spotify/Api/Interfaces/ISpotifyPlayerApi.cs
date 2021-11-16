namespace Cadenza.Source.Spotify;

internal interface ISpotifyPlayerApi
{
    Task<SpotifyApiPlayState> GetPlayState();
    Task Play(string trackId = null);
    Task Pause();
}