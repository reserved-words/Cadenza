using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Common.Interfaces;

public interface IAudioPlayer
{
    Task Play(string id);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task<TrackProgress> Stop();
}
