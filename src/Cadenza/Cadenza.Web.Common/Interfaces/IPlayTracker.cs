namespace Cadenza.Web.Common.Interfaces;

public interface IPlayTracker
{
    Task RecordPlay(TrackFullVM track, DateTime timestamp);
    Task UpdateNowPlaying(TrackFullVM track, int secondsRemaining);
}