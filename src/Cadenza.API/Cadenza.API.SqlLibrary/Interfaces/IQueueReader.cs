namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueReader
{
    Task<List<TrackRemovalRequest>> GetRemovalRequests(LibrarySource source);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
}