using Cadenza.Domain.Models;

namespace Cadenza.API.Common.Repositories;

public interface IUpdateRepository
{
    Task Add(ItemPropertyUpdate update);
    Task<List<ItemPropertyUpdate>> Get();
    Task Remove(ItemPropertyUpdate update);
}
