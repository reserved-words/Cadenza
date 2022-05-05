namespace Cadenza.Local.Common.Interfaces;

public interface IMusicDirectory
{
    Task<List<string>> GetAllFiles();
    Task<List<string>> GetModifiedFiles(DateTime sinceDate);
}
