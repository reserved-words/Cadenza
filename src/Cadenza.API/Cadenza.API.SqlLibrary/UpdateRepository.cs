using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;

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

    public async Task AddRemovalRequest(TrackRemovalRequest request)
    {
        await _updater.QueueRemoval(request);
    }

    public async Task AddUpdateRequest(ItemUpdateRequest request, LibrarySource? itemSource)
    {
        if (itemSource.HasValue)
        {
            await _updater.QueueUpdates(request, itemSource.Value);
        }
        else
        {
            foreach (var source in Enum.GetValues<LibrarySource>())
            {
                await _updater.QueueUpdates(request, source);
            }
        }
    }

    public async Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source)
    {
        return await _reader.GetRemovalRequests(source);
    }

    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
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

    public async Task MarkUpdateDone(ItemUpdateRequest request, LibrarySource source)
    {
        await _updater.MarkUpdateDone(request);
    }

    public async Task MarkUpdateErrored(ItemUpdateRequest request, LibrarySource source)
    {
        await _updater.MarkUpdateErrored(request);
    }
}
