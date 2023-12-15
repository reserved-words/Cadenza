using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class QueueRepository : IQueueRepository
{
    private readonly IQueueMapper _mapper;
    private readonly IQueue _queue;

    public QueueRepository(IQueue queue, IQueueMapper mapper)
    {
        _queue = queue;
        _mapper = mapper;
    }

    public async Task AddRemovalRequest(int trackId)
    {
        await _queue.AddTrackRemoval(trackId);
    }

    public async Task AddArtistUpdateRequest(int artistId)
    {
        await _queue.AddArtistUpdate(artistId);
    }

    public async Task AddAlbumUpdateRequest(int albumId)
    {
        await _queue.AddAlbumUpdate(albumId);
    }

    public async Task AddTrackUpdateRequest(int trackId)
    {
        await _queue.AddTrackUpdate(trackId);
    }

    public async Task<List<TrackRemovalSyncDTO>> GetRemovalRequests(LibrarySource source)
    {
        var requests = await _queue.GetTrackRemovals(source);
        return requests.Select(_mapper.MapSyncTrackRemovalRequest).ToList();
    }

    public async Task<List<ArtistUpdateSyncDTO>> GetArtistUpdateRequests(LibrarySource source)
    {
        var updates = await _queue.GetArtistUpdates(source);
        return _mapper.MapArtistUpdates(updates);
    }

    public async Task<List<AlbumUpdateSyncDTO>> GetAlbumUpdateRequests(LibrarySource source)
    {
        var updates = await _queue.GetAlbumUpdates(source);
        return _mapper.MapAlbumUpdates(updates);
    }

    public async Task<List<TrackUpdateSyncDTO>> GetTrackUpdateRequests(LibrarySource source)
    {
        var updates = await _queue.GetTrackUpdates(source);
        return _mapper.MapTrackUpdates(updates);
    }

    public async Task MarkRemovalDone(int requestId)
    {
        await _queue.MarkTrackRemovalDone(requestId);
    }

    public async Task MarkRemovalErrored(int requestId)
    {
        await _queue.MarkTrackRemovalErrored(requestId);
    }

    public async Task MarkArtistUpdateDone(int artistId)
    {
        await _queue.MarkArtistUpdateDone(artistId);
    }

    public async Task MarkAlbumUpdateDone(int albumId)
    {
        await _queue.MarkAlbumUpdateDone(albumId);
    }

    public async Task MarkTrackUpdateDone(int trackId)
    {
        await _queue.MarkTrackUpdateDone(trackId);
    }

    public async Task MarkArtistUpdateErrored(int artistId)
    {
        await _queue.MarkArtistUpdateErrored(artistId);
    }

    public async Task MarkAlbumUpdateErrored(int albumId)
    {
        await _queue.MarkAlbumUpdateErrored(albumId);
    }

    public async Task MarkTrackUpdateErrored(int trackId)
    {
        await _queue.MarkTrackUpdateErrored(trackId);
    }
}
