namespace Cadenza.API.Interfaces.Repositories;

public interface IUpdateRepository
{
    Task AddUpdateRequest(ItemUpdateRequest update, LibrarySource? itemSource);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateDone(ItemUpdateRequest request, LibrarySource source);
    Task MarkUpdateErrored(ItemUpdateRequest request, LibrarySource source);

    Task AddRemovalRequest(TrackRemovalRequest request);
    Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalDone(int requestId);
    Task MarkRemovalErrored(int requestId);
}
