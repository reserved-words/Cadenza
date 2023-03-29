using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary.Services;

internal class QueueReader : IQueueReader
{
    private readonly IDataReadService _readService;

    public QueueReader(IDataReadService readService)
    {
        _readService = readService;
    }

    public Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        throw new NotImplementedException();
    }
}
