namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueReader
{
    Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
}