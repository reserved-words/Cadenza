using Cadenza.Common.Domain.Model.Sync;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.Local.API.Common.Controllers;

public interface ISyncService
{
    Task<List<string>> GetAllTracks();
    Task<SyncTrack> GetTrack(string id);
    Task RemoveTrack(string trackId);
    Task UpdateTracks(MultiTrackUpdates updates);
}
