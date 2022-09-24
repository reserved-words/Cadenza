using Cadenza.Local.Common.Model;

namespace Cadenza.Local.Common.Interfaces;

public interface IMusicDirectory
{
    Task<List<LocalFile>> GetAllFiles();
    Task<List<LocalFile>> GetModifiedFiles(DateTime sinceDate);
}
