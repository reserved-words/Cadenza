using Cadenza.Domain;

namespace Cadenza.Common;

public interface IFileUpdateQueue
{
    Task<FileUpdateQueue> GetQueuedUpdates();
    Task<bool> RemoveQueuedUpdate(ItemPropertyUpdate update);
}
