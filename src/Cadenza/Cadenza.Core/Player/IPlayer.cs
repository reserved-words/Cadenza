namespace Cadenza.Core;

public interface IPlayer
{
    Task<TrackProgress> Play(BasicTrack track);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task<TrackProgress> Stop();
}
