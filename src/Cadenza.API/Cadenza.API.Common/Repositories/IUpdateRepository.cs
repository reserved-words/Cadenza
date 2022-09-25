using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;

namespace Cadenza.API.Common.Repositories;

public interface IUpdateRepository
{
    Task Add(ItemUpdates update);
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);
    Task Remove(LibrarySource source, ItemUpdates update);
}
