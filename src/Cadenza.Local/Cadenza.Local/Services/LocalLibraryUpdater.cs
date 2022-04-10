using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.FileProcessors;

namespace Cadenza.Local;

public class LocalLibraryUpdater : ILocalLibraryUpdater
{
    private readonly IAddedFilesHandler _addedFilesHandler;
    private readonly IModifiedFilesHandler _modifiedFilesHandler;
    private readonly IDeletedFilesHandler _deletedFilesHandler;
    private readonly IPlayedFilesHandler _playedFilesHandler;
    private readonly IUpdateQueueHandler _updateQueueHandler;

    public LocalLibraryUpdater(
        IModifiedFilesHandler modifiedFilesHandler,
        IDeletedFilesHandler deletedFilesHandler,
        IPlayedFilesHandler playedFilesHandler,
        IUpdateQueueHandler updateQueueHandler, 
        IAddedFilesHandler addedFilesHandler)
    {
        _modifiedFilesHandler = modifiedFilesHandler;
        _deletedFilesHandler = deletedFilesHandler;
        _playedFilesHandler = playedFilesHandler;
        _updateQueueHandler = updateQueueHandler;
        _addedFilesHandler = addedFilesHandler;
    }

    public async Task UpdateAddedFiles()
    {
        await _addedFilesHandler.Sync();
    }

    public async Task UpdateDeletedFiles()
    {
        await _deletedFilesHandler.Sync();
    }

    public async Task UpdateModifiedFiles()
    {
        await _modifiedFilesHandler.Sync();
    }

    public async Task RemovePlayedFiles()
    {
        await _playedFilesHandler.RemovePlayedFiles();
    }

    public async Task ProcessUpdateQueue()
    {
        await _updateQueueHandler.Process();
    }
}