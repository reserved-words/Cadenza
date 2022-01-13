namespace Cadenza.Common;

public interface IFileUpdateQueue
{
    Task<FileUpdateQueue> GetQueuedUpdates();
    Task<bool> RemoveQueuedUpdate(MetaDataUpdate update);
}
