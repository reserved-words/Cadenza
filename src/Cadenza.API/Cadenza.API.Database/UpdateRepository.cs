namespace Cadenza.API.Database;

internal class UpdateRepository : IUpdateRepository
{
    private readonly IDataAccess _dataAccess;
    private readonly IQueueUpdater _updater;

    public UpdateRepository(IDataAccess dataAccess, IQueueUpdater updater)
    {
        _dataAccess = dataAccess;
        _updater = updater;
    }

    public async Task Add(EditedItem update, LibrarySource? itemSource)
    {
        if (itemSource.HasValue)
        {
            await AddToSource(update, itemSource.Value);
        }
        else
        {
            foreach (var source in Enum.GetValues<LibrarySource>())
            {
                await AddToSource(update, source);
            }
        }
    }

    public async Task<List<EditedItem>> GetUpdates(LibrarySource source)
    {
        return await _dataAccess.GetUpdates(source);
    }

    public async Task Remove(EditedItem update, LibrarySource source)
    {
        await _dataAccess.UpdateUpdates(source, updates =>
        {
            _updater.Dequeue(updates, update);
        });
    }

    private async Task AddToSource(EditedItem update, LibrarySource source)
    {
        await _dataAccess.UpdateUpdates(source, updates =>
        {
            _updater.Queue(updates, update);
        });
    }
}