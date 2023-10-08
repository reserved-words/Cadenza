namespace Cadenza.API.SqlLibrary;

internal class UpdateRepository : IUpdateRepository
{
    private readonly IQueueReader _reader;
    private readonly IQueueUpdater _updater;

    public UpdateRepository(IQueueReader reader, IQueueUpdater updater)
    {
        _reader = reader;
        _updater = updater;
    }

    public async Task AddRemovalRequest(TrackRemovalRequestDTO request)
    {
        await _updater.QueueRemoval(request);
    }

    public async Task AddUpdateRequest(ItemUpdateRequestDTO request)
    {
        await _updater.QueueUpdates(request);
    }

    public async Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source)
    {
        return await _reader.GetRemovalRequests(source);
    }

    public async Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source)
    {
        return await _reader.GetUpdateRequests(source);
    }

    public async Task MarkRemovalDone(int requestId)
    {
        await _updater.MarkRemovalDone(requestId);
    }

    public async Task MarkRemovalErrored(int requestId)
    {
        await _updater.MarkRemovalErrored(requestId);
    }

    public async Task MarkUpdateDone(ItemUpdateRequestDTO request)
    {
        await _updater.MarkUpdateDone(request);
    }

    public async Task MarkUpdateErrored(ItemUpdateRequestDTO request)
    {
        await _updater.MarkUpdateErrored(request);
    }
}
