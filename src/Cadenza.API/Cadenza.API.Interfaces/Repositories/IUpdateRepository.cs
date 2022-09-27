using Cadenza.Domain.Model.Updates;

namespace Cadenza.API.Interfaces.Repositories;

public interface IUpdateRepository
{
    Task Add(ItemUpdates update, LibrarySource? itemSource);
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);
    Task Remove(ItemUpdates update, LibrarySource source);
}
