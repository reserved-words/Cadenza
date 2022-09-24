using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Model;
using Cadenza.Local.Config;
using Microsoft.Extensions.Options;

namespace Cadenza.Local.Services;

public class MusicDirectoryAccess : IMusicDirectory
{
    private readonly IOptions<MusicLibrary> _config;
    private readonly IFileAccess _fileAccess;

    public MusicDirectoryAccess(IOptions<MusicLibrary> config, IFileAccess fileAccess)
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