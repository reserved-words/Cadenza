using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Interfaces.Library;

public interface ITrackRepository
{
    Task<TrackFull> GetTrack(int id);
}
