namespace Cadenza.Database.Interfaces;

public interface IQueueRepository
{
    Task AddUpdateRequest(ItemUpdateRequestDTO update);
    Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateDone(ItemUpdateRequestDTO request);
    Task MarkUpdateErrored(ItemUpdateRequestDTO request);

    Task AddRemovalRequest(int trackId);
    Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalDone(int requestId);
    Task MarkRemovalErrored(int requestId);
}
