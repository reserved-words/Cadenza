namespace Cadenza.Local;

public interface IMusicDirectory
{
    List<string> GetAllFiles();
    List<string> GetModifiedFiles(DateTime sinceDate);
}