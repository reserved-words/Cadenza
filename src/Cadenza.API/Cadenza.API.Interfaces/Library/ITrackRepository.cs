using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Interfaces.Library;

public interface ITrackRepository
{
    Task<TrackFull> GetTrack(int id);
}
