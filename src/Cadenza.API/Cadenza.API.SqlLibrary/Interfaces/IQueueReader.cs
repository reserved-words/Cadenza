namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueReader
{
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
}