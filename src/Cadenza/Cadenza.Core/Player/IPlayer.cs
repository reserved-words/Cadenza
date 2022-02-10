namespace Cadenza.Core;

public interface IPlayer
{
    Task<TrackProgress> Play(PlayTrack track);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task Stop();
}
