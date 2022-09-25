using Cadenza.API.Common.Controllers;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Core.Services;
internal class SyncService : ISyncService
{
    public Task AddTrack(LibrarySource source, TrackFull track)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetAllTracks(LibrarySource source)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        throw new NotImplementedException();
    }

    public Task MarkUpdated(LibrarySource source, LibraryItemType itemType, string id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTrack(LibrarySource source, string id)
    {
        throw new NotImplementedException();
    }
}
