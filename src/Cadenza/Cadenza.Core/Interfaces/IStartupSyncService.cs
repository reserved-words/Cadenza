using Cadenza.Core.Tasks;

namespace Cadenza
{
    public interface IStartupSyncService
    {
        TaskGroup GetStartupTasks();
    }
}
