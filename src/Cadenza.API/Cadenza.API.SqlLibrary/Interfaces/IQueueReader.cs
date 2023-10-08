using Cadenza.Common.Enums;

namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IQueueReader
{
    Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source);
    Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source);
}