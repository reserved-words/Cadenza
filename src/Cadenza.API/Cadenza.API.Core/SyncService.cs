using Cadenza.API.Common.Controllers;
using Cadenza.API.Common.Repositories;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Core;
internal class SyncService : ISyncService
{
    private readonly IMusicRepository _repository;

    public SyncService(IMusicRepository repository)
    {
        _repository = repository;
    }

    public Task AddTrack(LibrarySource source, TrackFull track)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        var library = await _repository.Get(source);

        return library.Tracks
            .Select(t => t.Id)
            .ToList();
    }

    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId)
    {
        var library = await _repository.Get(source);
        
        return library.Tracks
            .Where(t => t.AlbumId == albumId)
            .Select(t => t.Id)
            .ToList();
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
