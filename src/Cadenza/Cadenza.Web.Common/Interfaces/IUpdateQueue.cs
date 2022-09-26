using Cadenza.Domain.Models.Updates;

namespace Cadenza.Web.Common.Interfaces;

public interface IUpdateQueue
{
    Task<List<ItemUpdates>> GetQueuedUpdates();
    Task<bool> RemoveQueuedUpdate(ItemUpdates update);
}
