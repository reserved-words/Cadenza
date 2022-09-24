using Cadenza.Core.Model;
using Cadenza.Domain.Models;

namespace Cadenza.Core.Interfaces;

public interface IFileUpdateQueue
{
    Task<FileUpdateQueue> GetQueuedUpdates();
    Task<bool> RemoveQueuedUpdate(ItemPropertyUpdate update);
}
