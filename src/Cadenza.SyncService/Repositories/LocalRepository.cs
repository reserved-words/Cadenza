using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;
using Cadenza.SyncService.Interfaces;

namespace Cadenza.SyncService.Repositories;

internal class LocalRepository : ISourceRepository
{
    public LibrarySource Source => LibrarySource.Local;

    public Task<List<string>> GetAllTracks()
    {
        throw new NotImplementedException();
    }

    public Task<TrackFull> GetTrack(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTrack(string id, List<PropertyUpdate> updates)
    {
        throw new NotImplementedException();
    }
}
