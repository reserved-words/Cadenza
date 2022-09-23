using Cadenza.Local.Common.Interfaces;

namespace Cadenza.Local.Services;

public class UpdatedFilesFetcher : IUpdatedFilesFetcher
{
    private readonly IMusicDirectory _musicDirectory;
    private readonly IListComparer _listComparer;

    public UpdatedFilesFetcher(IMusicDirectory musicDirectory, IListComparer listComparer)
    {
        _musicDirectory = musicDirectory;
        _listComparer = listComparer;
    }

    public Task<List<string>> GetAddedFiles()
    {
        throw new NotImplementedException();

        //var filesInDirectory = await _musicDirectory.GetAllFiles();
        //var tracks = await _jsonData.GetTracks(LibrarySource.Local);
        //var pathsInJson = tracks.Select(t => t.Path).ToList();
        //return _listComparer.GetMissingItems(filesInDirectory, pathsInJson);
    }

    public Task<List<string>> GetModifiedFiles()
    {
        throw new NotImplementedException();

        //var lastUpdate = await _updateHistory.GetDateLastUpdated(LibrarySource.Local);
        //return await _musicDirectory.GetModifiedFiles(lastUpdate);
    }

    public Task<List<string>> GetRemovedFiles()
    {
        throw new NotImplementedException();

        //var filesInDirectory = await _musicDirectory.GetAllFiles();
        //var tracks = await _jsonData.GetTracks(LibrarySource.Local);
        //var pathsInJson = tracks.Select(t => t.Path).ToList();
        //return _listComparer.GetMissingItems(pathsInJson, filesInDirectory);
    }

    public Task UpdateTimeModifiedFilesUpdated(DateTime updateTime)
    {
        throw new NotImplementedException();

        //await _updateHistory.UpdateDateLastUpdated(updateTime, LibrarySource.Local);
    }
}