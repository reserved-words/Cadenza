namespace Cadenza.Local.Common.Interfaces;

public interface IUpdatedFilesFetcher
{
    Task<List<string>> GetAddedFiles();
    Task<List<string>> GetModifiedFiles();
    Task UpdateTimeModifiedFilesUpdated(DateTime updateTime);
    Task<List<string>> GetRemovedFiles();
}
