using Cadenza.Domain.Models;

namespace Cadenza.API.Common.Controllers;

public interface IUpdateService
{
    Task<List<ItemUpdates>> GetQueuedUpdates();
    Task Update(ItemUpdates update);
}
