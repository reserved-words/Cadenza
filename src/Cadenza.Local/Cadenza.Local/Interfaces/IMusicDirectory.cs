namespace Cadenza.Local;

public interface IMusicDirectory
{
    Task<List<string>> GetAllFiles();
    Task<List<string>> GetModifiedFiles(DateTime sinceDate);
}