namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueReader
{
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);
}