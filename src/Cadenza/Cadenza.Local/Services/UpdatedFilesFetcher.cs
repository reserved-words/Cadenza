namespace Cadenza.Local;

public class UpdatedFilesFetcher : IUpdatedFilesFetcher
{
    private readonly IMusicDirectory _musicDirectory;
    private readonly IDataAccess _jsonData;
    private readonly IListComparer _listComparer;
    private readonly IUpdateHistory _updateHistory;

    public UpdatedFilesFetcher(IDataAccess jsonData, IMusicDirectory musicDirectory, IListComparer listComparer, IUpdateHistory updateHistory)
    {
        _jsonData = jsonData;
        _musicDirectory = musicDirectory;
        _listComparer = listComparer;
        _updateHistory = updateHistory;
    }

    public List<string> GetAddedFiles()
    {
        var filesInDirectory = _musicDirectory.GetAllFiles();
        var pathsInJson = _jsonData.GetTracks().Select(t => t.Path).ToList();
        return _listComparer.GetMissingItems(filesInDirectory, pathsInJson);
    }

    public List<string> GetFilesModifiedSinceLastUpdate()
    {
        var lastUpdate = _updateHistory.GetDateProcessedModifiedFiles();
        return _musicDirectory.GetModifiedFiles(lastUpdate);
    }

    public List<string> GetRemovedFiles()
    {
        var filesInDirectory = _musicDirectory.GetAllFiles();
        var pathsInJson = _jsonData.GetTracks().Select(t => t.Path).ToList();
        return _listComparer.GetMissingItems(pathsInJson, filesInDirectory);
    }

    public void UpdateTimeModifiedFilesUpdated(DateTime updateTime)
    {
        _updateHistory.SetDateProcessedModifiedFiles(updateTime);
    }
}