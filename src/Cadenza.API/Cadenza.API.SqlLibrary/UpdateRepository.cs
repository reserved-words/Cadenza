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

    public async Task Add(ItemUpdates update, LibrarySource? itemSource)
    {
        if (itemSource.HasValue)
        {
            await _updater.QueueUpdates(update, itemSource.Value);
        }
        else
        {
            foreach (var source in Enum.GetValues<LibrarySource>())
            {
                await _updater.QueueUpdates(update, source);
            }
        }
    }

    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        return await _reader.GetUpdates(source);
    }

    public async Task Remove(ItemUpdates update, LibrarySource source)
    {
        await _updater.MarkUpdatesDone(update);
    }
}
