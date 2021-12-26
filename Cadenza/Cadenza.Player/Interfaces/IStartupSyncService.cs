namespace Cadenza.Player
{
    public interface IStartupSyncService
    {
        TaskGroup GetLibrarySyncTasks();
    }
}
