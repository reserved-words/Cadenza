namespace Cadenza.API.Interfaces.Repositories;

public interface IUpdateRepository
{
    Task AddUpdateRequest(ItemUpdateRequestDTO update);
    Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateDone(ItemUpdateRequestDTO request);
    Task MarkUpdateErrored(ItemUpdateRequestDTO request);

    Task AddRemovalRequest(TrackRemovalRequestDTO request);
    Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalDone(int requestId);
    Task MarkRemovalErrored(int requestId);
}
