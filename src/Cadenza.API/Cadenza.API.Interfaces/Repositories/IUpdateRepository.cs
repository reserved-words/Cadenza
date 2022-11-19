namespace Cadenza.API.Interfaces.Repositories;

public interface IUpdateRepository
{
    Task Add(EditedItem update, LibrarySource? itemSource);
    Task<List<EditedItem>> GetUpdates(LibrarySource source);
    Task Remove(EditedItem update, LibrarySource source);
}
