using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary.Services;

internal class QueueUpdater : IQueueUpdater
{
    private readonly IDataInsertService _insertionService;
    private readonly IDataDeletionService _deletionService;

    public QueueUpdater(IDataInsertService insertionService, IDataDeletionService deletionService)
    {
        _insertionService = insertionService;
        _deletionService = deletionService;
    }

    public Task MarkUpdatesDone(ItemUpdates updates)
    {
        throw new NotImplementedException();
    }

    public Task QueueUpdates(ItemUpdates updates)
    {
        throw new NotImplementedException();
    }
}