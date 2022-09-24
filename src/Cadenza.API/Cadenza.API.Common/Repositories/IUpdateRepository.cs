using Cadenza.Domain;

namespace Cadenza.API.Common.Repositories;

public interface IUpdateRepository
{
    Task Add(ItemPropertyUpdate update);
    Task<List<ItemPropertyUpdate>> Get();
    Task Remove(ItemPropertyUpdate update);
}
