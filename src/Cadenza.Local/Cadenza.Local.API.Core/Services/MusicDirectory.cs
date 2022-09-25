using Cadenza.Domain.Models;
using Cadenza.Local.API.Core.Config;
using Cadenza.Local.API.Core.Interfaces;
using Cadenza.Utilities.Interfaces;
using Microsoft.Extensions.Options;

namespace Cadenza.Local.API.Core.Services;

internal class MusicDirectory : IMusicDirectory
{
    private readonly IOptions<MusicLibrarySettings> _config;
    private readonly IFileAccess _fileAccess;

    public MusicDirectory(IOptions<MusicLibrarySettings> config, IFileAccess fileAccess)
    {
        _config = config;
        _fileAccess = fileAccess;
    }

    public async Task<List<FileDetails>> GetAllFiles()
    {
        return (await GetFiles()).ToList();
    }

    public async Task<List<FileDetails>> GetModifiedFiles(DateTime sinceDate)
    {
        var files = await GetFiles();
        return files
            .Where(f => f.DateModified > sinceDate)
            .ToList();
    }

    private async Task<IEnumerable<FileDetails>> GetFiles()
    {
        var directoryPath = _config.Value.Directory;
        var extensions = _config.Value.FileExtensions;

        return await _fileAccess.GetFiles(directoryPath, extensions);
    }
}