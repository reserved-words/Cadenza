
namespace Cadenza.Common;

public interface IPlayTracker
{
    Task RecordPlay(TrackFull track, DateTime timestamp);
    Task UpdateNowPlaying(TrackFull track, int duration);
}