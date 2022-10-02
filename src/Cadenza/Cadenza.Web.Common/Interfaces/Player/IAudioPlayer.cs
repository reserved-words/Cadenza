namespace Cadenza.Web.Common.Interfaces.Player;

public interface IAudioPlayer
{
    Task Play(string id);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task<TrackProgress> Stop();
}
