using Cadenza.Web.Model;

namespace Cadenza.Web.Common.Interfaces;

public interface ITrackRepository
{
    Task<TrackFullVM> GetTrack(int id);
}
