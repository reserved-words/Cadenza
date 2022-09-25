using Cadenza.Domain.Models;

namespace Cadenza.Utilities.Interfaces;

public interface IFileAccess
{
    Task DeleteFile(string path);
    Task<List<FileDetails>> GetFiles(string directoryPath, List<string> extensions = null);
    Task<string> GetText(string path);
    Task SaveText(string path, string text);
}
