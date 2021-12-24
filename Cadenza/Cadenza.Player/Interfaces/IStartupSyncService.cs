namespace Cadenza.Player
{
    public interface IStartupSyncService
    {
        event ProgressEventHandler ProgressChanged;
        event SyncProgressEventHandler SyncProgressChanged;

        Task SyncLibrary(CancellationToken cancellationToken);
    }
}
