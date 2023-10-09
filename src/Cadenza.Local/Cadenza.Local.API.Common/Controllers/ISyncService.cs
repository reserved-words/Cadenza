using Cadenza.Common.DTO;

namespace Cadenza.Local.API.Common.Controllers;

public interface ISyncService
{
    Task<List<string>> GetAllTracks();
    Task<SyncTrackDTO> GetTrack(string id);
    Task RemoveTrack(string trackId);
    Task UpdateTracks(MultiTrackUpdatesDTO updates);
}
