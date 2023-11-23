namespace Cadenza.Database.SqlLibrary.Repositories;

internal class QueueRepository : IQueueRepository
{
    private readonly IMapper _mapper;
    private readonly IQueue _queue;

    public QueueRepository(IQueue queue, IMapper mapper)
    {
        _queue = queue;
        _mapper = mapper;
    }

    public async Task AddRemovalRequest(int trackId)
    {
        await _queue.AddTrackRemoval(trackId);
    }

    public async Task AddUpdateRequest(ItemUpdateRequestDTO request)
    {
        Func<ItemUpdateRequestDTO, PropertyUpdateDTO, Task> queue = request.Type switch
        {
            LibraryItemType.Artist => QueueArtistUpdate,
            LibraryItemType.Album => QueueAlbumUpdate,
            LibraryItemType.Track => QueueTrackUpdate,
            _ => throw new NotImplementedException()
        };

        foreach (var update in request.Updates)
        {
            await queue(request, update);
        }
    }

    public async Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source)
    {
        var requests = await _queue.GetTrackRemovals(source);
        return requests.Select(_mapper.MapSyncTrackRemovalRequest).ToList();
    }

    public async Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source)
    {
        var artistUpdates = await _queue.GetArtistUpdates(source);
        var albumUpdates = await _queue.GetAlbumUpdates(source);
        var trackUpdates = await _queue.GetTrackUpdates(source);

        return _mapper.MapArtistUpdateRequests(artistUpdates)
            .Concat(_mapper.MapAlbumUpdateRequests(albumUpdates))
            .Concat(_mapper.MapTrackUpdateRequests(trackUpdates))
            .ToList();
    }

    public async Task MarkRemovalDone(int requestId)
    {
        await _queue.MarkTrackRemovalDone(requestId);
    }

    public async Task MarkRemovalErrored(int requestId)
    {
        await _queue.MarkTrackRemovalErrored(requestId);
    }

    public async Task MarkUpdateDone(ItemUpdateRequestDTO request)
    {
        Func<int, Task> markAsDone = request.Type switch
        {
            LibraryItemType.Artist => _queue.MarkArtistUpdateDone,
            LibraryItemType.Album => _queue.MarkAlbumUpdateDone,
            LibraryItemType.Track => _queue.MarkTrackUpdateDone,
            _ => throw new NotImplementedException()
        };

        foreach (var update in request.Updates)
        {
            await markAsDone(update.Id);
        }
    }

    public async Task MarkUpdateErrored(ItemUpdateRequestDTO request)
    {
        Func<int, Task> markAsErrored = request.Type switch
        {
            LibraryItemType.Artist => _queue.MarkArtistUpdateErrored,
            LibraryItemType.Album => _queue.MarkAlbumUpdateErrored,
            LibraryItemType.Track => _queue.MarkTrackUpdateErrored,
            _ => throw new NotImplementedException()
        };

        foreach (var update in request.Updates)
        {
            await markAsErrored(update.Id);
        }
    }

    private async Task QueueArtistUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        var artistUpdate = _mapper.MapArtistUpdate(request, update);
        await _queue.AddArtistUpdate(artistUpdate);
    }

    private async Task QueueAlbumUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        var albumUpdate = _mapper.MapAlbumUpdate(request, update);
        await _queue.AddAlbumUpdate(albumUpdate);
    }

    private async Task QueueTrackUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        var trackUpdate = _mapper.MapTrackUpdate(request, update);
        await _queue.AddTrackUpdate(trackUpdate);
    }
}
