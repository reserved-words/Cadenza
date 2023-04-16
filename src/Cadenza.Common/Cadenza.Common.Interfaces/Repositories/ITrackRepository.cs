using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.Common.Interfaces.Repositories;

public interface ITrackRepository
{
    Task<TrackFull> GetTrack(int id);
}
