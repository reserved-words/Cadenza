namespace Cadenza.API.Interfaces.Repositories;

public interface IUpdateRepository
{
    Task Add(ItemUpdateRequest update, LibrarySource? itemSource);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
    Task MarkAsDone(ItemUpdateRequest request, LibrarySource source);
    Task MarkAsErrored(ItemUpdateRequest request, LibrarySource source);
}
