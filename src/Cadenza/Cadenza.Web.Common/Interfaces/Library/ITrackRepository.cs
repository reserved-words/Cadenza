using Cadenza.Web.Model;

namespace Cadenza.Web.Common.Interfaces.Library;

public interface ITrackRepository
{
    Task<TrackFullVM> GetTrack(int id);
}
