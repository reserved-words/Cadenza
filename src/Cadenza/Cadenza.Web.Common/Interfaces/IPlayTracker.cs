
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.Web.Common.Interfaces;

public interface IPlayTracker
{
    Task RecordPlay(TrackFull track, DateTime timestamp);
    Task UpdateNowPlaying(TrackFull track, int duration);
}