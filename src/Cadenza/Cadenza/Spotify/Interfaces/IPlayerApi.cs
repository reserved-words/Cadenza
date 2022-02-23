using Cadenza.Core.Model;

namespace Cadenza.Source.Spotify.Player;

public interface IPlayerApi
{
    Task<TrackProgress> Pause(string accessToken);
    Task Play(string trackId, string accessToken);
    Task<TrackProgress> Resume(string accessToken);
    Task<TrackProgress> Stop(string accessToken);
}
