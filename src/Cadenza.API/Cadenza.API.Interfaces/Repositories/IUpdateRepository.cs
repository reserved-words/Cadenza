namespace Cadenza.API.Interfaces.Repositories;

public interface IUpdateRepository
{
    Task AddUpdateRequest(ItemUpdateRequest update);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateDone(ItemUpdateRequest request);
    Task MarkUpdateErrored(ItemUpdateRequest request);

    Task AddRemovalRequest(TrackRemovalRequest request);
    Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalDone(int requestId);
    Task MarkRemovalErrored(int requestId);
}
