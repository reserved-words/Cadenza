using Cadenza.Domain.Models;
using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Common.Interfaces;

public interface IFileUpdateQueue
{
    Task<FileUpdateQueue> GetQueuedUpdates();
    Task<bool> RemoveQueuedUpdate(ItemPropertyUpdate update);
}
