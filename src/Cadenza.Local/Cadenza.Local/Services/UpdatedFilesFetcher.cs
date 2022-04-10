using Cadenza.Local.Common.Interfaces;

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

    public async Task<List<string>> GetAddedFiles()
    {
        var filesInDirectory = await _musicDirectory.GetAllFiles();
        var tracks = await _jsonData.GetTracks(LibrarySource.Local);
        var pathsInJson = tracks.Select(t => t.Path).ToList();
        return _listComparer.GetMissingItems(filesInDirectory, pathsInJson);
    }

    public async Task<List<string>> GetFilesModifiedSinceLastUpdate()
    {
        var lastUpdate = await _updateHistory.GetDateLastUpdated(LibrarySource.Local);
        return await _musicDirectory.GetModifiedFiles(lastUpdate);
    }

    public async Task<List<string>> GetRemovedFiles()
    {
        var filesInDirectory = await _musicDirectory.GetAllFiles();
        var tracks = await _jsonData.GetTracks(LibrarySource.Local);
        var pathsInJson = tracks.Select(t => t.Path).ToList();
        return _listComparer.GetMissingItems(pathsInJson, filesInDirectory);
    }

    public async Task UpdateTimeModifiedFilesUpdated(DateTime updateTime)
    {
        await _updateHistory.UpdateDateLastUpdated(updateTime, LibrarySource.Local);
    }
}