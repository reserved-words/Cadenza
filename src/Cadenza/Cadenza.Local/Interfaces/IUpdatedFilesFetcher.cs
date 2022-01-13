namespace Cadenza.Local;

public interface IUpdatedFilesFetcher
{
    List<string> GetAddedFiles();
    List<string> GetFilesModifiedSinceLastUpdate();
    void UpdateTimeModifiedFilesUpdated(DateTime updateTime);
    List<string> GetRemovedFiles();
}
