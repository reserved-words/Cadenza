using Cadenza.Source.Spotify.Api.Model.Player;

namespace Cadenza.Source.Spotify.Api.Interfaces;

public interface IPlayerApi
{
    Task Pause(string deviceId);
    Task Play(string deviceId, string trackId = null);
    Task<SpotifyApiPlayState> GetPlayState();
}
