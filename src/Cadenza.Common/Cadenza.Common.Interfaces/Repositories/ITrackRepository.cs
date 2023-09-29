using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Common.Interfaces.Repositories;

public interface ITrackRepository
{
    Task<TrackFull> GetTrack(int id);
}
