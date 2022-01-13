namespace Cadenza.Common;

public interface IPlayTracker
{
    Task RecordPlay(PlayingTrack track, DateTime timestamp);
    Task UpdateNowPlaying(PlayingTrack track, int duration);
}