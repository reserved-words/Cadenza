using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;

namespace Cadenza.Local.API.Common.Controllers;

public interface ISyncService
{
    Task<List<string>> GetAllTracks();
    Task<TrackFull> GetTrack(string id);
    Task UpdateTrack(string id, List<PropertyUpdate> updates);
}
