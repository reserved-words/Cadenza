using Cadenza.Core.Model;

namespace Cadenza.Core.Interfaces;

public interface IAudioPlayer
{
    Task Play(string id);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task<TrackProgress> Stop();
}
