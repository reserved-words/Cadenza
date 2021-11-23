namespace Cadenza.Player
{
    public interface IStartupSyncService
    {
        event ProgressEventHandler ProgressChanged;

        Task SyncLibrary(CancellationToken cancellationToken);
    }
}
