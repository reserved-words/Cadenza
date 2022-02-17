namespace Cadenza.Local;

public interface IUpdatedFilesFetcher
{
    Task<List<string>> GetAddedFiles();
    Task<List<string>> GetFilesModifiedSinceLastUpdate();
    Task UpdateTimeModifiedFilesUpdated(DateTime updateTime);
    Task<List<string>> GetRemovedFiles();
}
