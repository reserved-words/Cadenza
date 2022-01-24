namespace Cadenza.Common;

public interface IAudioPlayer
{
    Task Play(string id);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task Stop();
}
