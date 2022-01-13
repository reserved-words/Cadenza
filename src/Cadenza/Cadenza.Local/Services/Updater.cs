namespace Cadenza.Local;

public class Updater : IUpdater
{
    private readonly IAddedFilesHandler _addedFilesHandler;
    private readonly IModifiedFilesHandler _modifiedFilesHandler;
    private readonly IDeletedFilesHandler _deletedFilesHandler;
    private readonly IPlayedFilesHandler _playedFilesHandler;
    private readonly IUpdateQueueHandler _updateQueueHandler;

    public Updater(IAddedFilesHandler addedFilesHandler,
        IModifiedFilesHandler modifiedFilesHandler,
        IDeletedFilesHandler deletedFilesHandler,
        IPlayedFilesHandler playedFilesHandler,
        IUpdateQueueHandler updateQueueHandler)
    {
        _addedFilesHandler = addedFilesHandler;
        _modifiedFilesHandler = modifiedFilesHandler;
        _deletedFilesHandler = deletedFilesHandler;
        _playedFilesHandler = playedFilesHandler;
        _updateQueueHandler = updateQueueHandler;
    }

    public void UpdateAddedFiles()
    {
        _addedFilesHandler.Sync();
    }

    public void UpdateDeletedFiles()
    {
        _deletedFilesHandler.Sync();
    }

    public void UpdateModifiedFiles()
    {
        _modifiedFilesHandler.Sync();
    }

    public void RemovePlayedFiles()
    {
        _playedFilesHandler.RemovePlayedFiles();
    }

    public void ProcessUpdateQueue()
    {
        _updateQueueHandler.Process();
    }
}