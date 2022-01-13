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

    public void Sync()
    {
        var filepaths = _fileFetcher.GetRemovedFiles();

        if (!filepaths.Any())
            return;

        var jsonData = _dataAccess.GetAll();

        _organiser.RemoveTracks(jsonData, filepaths);

        _dataAccess.SaveAll(jsonData);
    }
}