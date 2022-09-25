using Cadenza.Local.API.Core.Config;
using Cadenza.Local.API.Core.Interfaces;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Model;
using Microsoft.Extensions.Options;

namespace Cadenza.Local.API.Core.Services;

internal class MusicDirectoryAccess : IMusicDirectory
{
    private readonly IOptions<MusicLibrarySettings> _config;
    private readonly IFileAccess _fileAccess;

    public MusicDirectoryAccess(IOptions<MusicLibrarySettings> config, IFileAccess fileAccess)
    {
        _config = config;
        _fileAccess = fileAccess;
    }

    public async Task<List<LocalFile>> GetAllFiles()
    {
        return (await GetFiles()).ToList();
    }

    public async Task<List<LocalFile>> GetModifiedFiles(DateTime sinceDate)
    {
        var files = await GetFiles();
        return files
            .Where(f => f.DateModified > sinceDate)
            .ToList();
    }

    private async Task<IEnumerable<LocalFile>> GetFiles()
    {
        var directoryPath = _config.Value.Directory;
        var extensions = _config.Value.FileExtensions;

        return await _fileAccess.GetFiles(directoryPath, extensions);
    }
}