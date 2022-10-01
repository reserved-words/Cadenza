using Cadenza.Common.Domain.Model.Track;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.Local.API.Common.Controllers;

public interface ISyncService
{
    Task<List<string>> GetAllTracks();
    Task<TrackFull> GetTrack(string id);
    Task UpdateTracks(MultiTrackUpdates updates);
}
