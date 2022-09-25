using Cadenza.Domain.Models;

namespace Cadenza.Web.Common.Interfaces;

public interface IUpdateQueue
{
    Task<List<ItemUpdates>> GetQueuedUpdates();
    Task<bool> RemoveQueuedUpdate(ItemUpdates update);
}
