namespace Cadenza.Common;

public interface IAudioPlayer
{
    Task<TrackProgress> Play(string id);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task<TrackProgress> Stop();
}
