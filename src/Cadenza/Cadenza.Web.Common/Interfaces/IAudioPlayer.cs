namespace Cadenza.Web.Common.Interfaces;

public interface IAudioPlayer
{
    Task Play(string id, string track, string artist, string playlist, string artworkUrl);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task<TrackProgress> Stop();
}
