using Cadenza.Common;

namespace Cadenza
{
    public interface IStartupSyncService
    {
        TaskGroup GetLibrarySyncTasks();
    }
}
