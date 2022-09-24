using Cadenza.Domain;
using Cadenza.Local.Common.Interfaces;

namespace Cadenza.Local.SyncService.Updaters;

public class DeletedFilesHandler : IUpdateService
{
    private readonly IUpdatedFilesFetcher _fileFetcher;

    public DeletedFilesHandler(IUpdatedFilesFetcher fileFetcher)
    {
        _fileFetcher = fileFetcher;
    }

    public Task Run()
    {
        throw new NotImplementedException();

        //var filepaths = await _fileFetcher.GetRemovedFiles();

        //if (!filepaths.Any())
        //    return;

        //var jsonData = await _dataAccess.GetAll(LibrarySource.Local);

        //_organiser.RemoveTracks(jsonData, filepaths);

        //await _dataAccess.SaveAll(jsonData, LibrarySource.Local);
    }
}