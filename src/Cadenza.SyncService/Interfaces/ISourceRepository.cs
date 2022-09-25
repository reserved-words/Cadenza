using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;

namespace Cadenza.SyncService.Interfaces;

internal interface ISourceRepository
{
    LibrarySource Source { get; }

    Task<List<string>> GetAllTracks();
    Task<TrackFull> GetTrack(string id);
    Task UpdateTrack(string trackId, ItemUpdates updates);
}
