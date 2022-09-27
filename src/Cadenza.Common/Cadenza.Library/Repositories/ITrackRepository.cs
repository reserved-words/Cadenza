using Cadenza.Domain.Model.Track;

namespace Cadenza.Library.Repositories;

public interface ITrackRepository
{
    Task<TrackFull> GetTrack(string id);
}
