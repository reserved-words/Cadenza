using Cadenza.Domain.Models.Track;
using Cadenza.Domain.Models.Updates;

namespace Cadenza.Local.API.Common.Controllers;

public interface ISyncService
{
    Task<List<string>> GetAllTracks();
    Task<TrackFull> GetTrack(string id);
    Task UpdateTracks(MultiTrackUpdates updates);
}
