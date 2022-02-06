namespace Cadenza.Local;

public class DeletedFilesHandler : IDeletedFilesHandler
{
    private readonly IDataAccess _dataAccess;
    private readonly IUpdatedFilesFetcher _fileFetcher;
    private readonly ILibraryOrganiser _organiser;

    public DeletedFilesHandler(IUpdatedFilesFetcher fileFetcher, ILibraryOrganiser organiser, IDataAccess dataAccess)
    {
        _fileFetcher = fileFetcher;
        _organiser = organiser;
        _dataAccess = dataAccess;
    }

    public async Task Sync()
    {
        var filepaths = await _fileFetcher.GetRemovedFiles();

        if (!filepaths.Any())
            return;

        var jsonData = await _dataAccess.GetAll();

        _organiser.RemoveTracks(jsonData, filepaths);

        await _dataAccess.SaveAll(jsonData);
    }
}