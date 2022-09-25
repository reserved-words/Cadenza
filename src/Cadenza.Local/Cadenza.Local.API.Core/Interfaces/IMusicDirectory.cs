using Cadenza.Local.Common.Model;

namespace Cadenza.Local.API.Core.Interfaces;

internal interface IMusicDirectory
{
    Task<List<LocalFile>> GetAllFiles();
    Task<List<LocalFile>> GetModifiedFiles(DateTime sinceDate);
}
