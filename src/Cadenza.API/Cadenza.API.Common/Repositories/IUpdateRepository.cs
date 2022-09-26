using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;

namespace Cadenza.API.Common.Repositories;

public interface IUpdateRepository
{
    Task Add(ItemUpdates update, LibrarySource? itemSource);
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);
    Task Remove(ItemUpdates update, LibrarySource source);
}
