namespace Cadenza.Core
{
    public interface IStartupSyncService
    {
        TaskGroup GetLibrarySyncTasks();
    }
}
