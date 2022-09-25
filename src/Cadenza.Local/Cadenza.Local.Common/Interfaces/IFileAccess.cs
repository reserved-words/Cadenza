using Cadenza.Local.Common.Model;

namespace Cadenza.Local.Common.Interfaces;

public interface IFileAccess
{
    Task<List<LocalFile>> GetFiles(string directoryPath, List<string> extensions = null);
    Task DeleteFile(string path);
}
