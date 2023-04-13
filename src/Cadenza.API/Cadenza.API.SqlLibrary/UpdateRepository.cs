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

    public async Task Add(ItemUpdateRequest request, LibrarySource? itemSource)
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

    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
    {
        return await _reader.GetUpdateRequests(source);
    }

    public async Task MarkAsDone(ItemUpdateRequest request, LibrarySource source)
    {
        await _updater.MarkUpdatesDone(request);
    }

    public async Task MarkAsErrored(ItemUpdateRequest request, LibrarySource source)
    {
        await _updater.MarkUpdatesErrored(request);
    }
}
