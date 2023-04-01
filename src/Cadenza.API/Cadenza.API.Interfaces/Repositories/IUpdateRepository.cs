namespace Cadenza.API.Interfaces.Repositories;

public interface IUpdateRepository
{
    Task Add(ItemUpdates update, LibrarySource? itemSource);
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);
    Task MarkAsDone(ItemUpdates update, LibrarySource source);
    Task MarkAsErrored(ItemUpdates update, LibrarySource source);
}
