namespace Cadenza.Local.FileAccess;

public interface IFileAccess
{
    void DeleteFile(string path);
    void MoveFile(string sourcePath, string targetPath);
    List<FileDetails> GetFiles(string directoryPath, List<string> extensions = null);
}
