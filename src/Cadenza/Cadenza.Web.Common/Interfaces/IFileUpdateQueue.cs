using Cadenza.Domain.Models;

namespace Cadenza.Web.Common.Interfaces;

public interface IFileUpdateQueue
{
    Task<List<ItemPropertyUpdate>> GetQueuedUpdates();
    Task<bool> RemoveQueuedUpdate(ItemPropertyUpdate update);
}
