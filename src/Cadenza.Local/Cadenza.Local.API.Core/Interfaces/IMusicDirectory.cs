using Cadenza.Domain.Models;

namespace Cadenza.Local.API.Core.Interfaces;

internal interface IMusicDirectory
{
    Task<List<FileDetails>> GetAllFiles();
    Task<List<FileDetails>> GetModifiedFiles(DateTime sinceDate);
}
