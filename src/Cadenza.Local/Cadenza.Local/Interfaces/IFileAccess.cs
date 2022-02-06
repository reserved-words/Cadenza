namespace Cadenza.Local;

public interface IFileAccess
{
    Task<List<LocalFile>> GetFiles(string directoryPath, List<string> extensions);
    Task<string> GetText(string path);
    Task SaveText(string path, string text);
    Task DeleteFile(string path);
}
