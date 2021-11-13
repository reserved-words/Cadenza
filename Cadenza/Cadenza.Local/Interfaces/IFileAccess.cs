namespace Cadenza.Local;

public interface IFileAccess
{
    IEnumerable<LocalFile> GetFiles(string directoryPath, List<string> extensions);
    string GetText(string path);
    void SaveText(string path, string text);
    void DeleteFile(string path);
}
