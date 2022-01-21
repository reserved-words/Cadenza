using Cadenza.Domain;

namespace Cadenza.Common;

public interface IPlayTracker
{
    Task RecordPlay(TrackSummary track, DateTime timestamp);
    Task UpdateNowPlaying(TrackSummary track, int duration);
}