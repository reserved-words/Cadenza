using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Track;
using Cadenza.Domain.Models.Updates;

namespace Cadenza.SyncService.Interfaces;

internal interface ISourceRepository
{
    LibrarySource Source { get; }

    Task<List<string>> GetAllTracks();
    Task<TrackFull> GetTrack(string id);
    Task UpdateTracks(List<string> trackIds, List<PropertyUpdate> updates);
}
