using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Utilities;

public interface IFileAccess
{
    void DeleteFile(string path);
    void MoveFile(string sourcePath, string targetPath);
    List<FileDetails> GetFiles(string directoryPath, List<string> extensions = null);
    string GetText(string path);
    void SaveText(string path, string text);
}
