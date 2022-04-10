using Microsoft.Extensions.Options;

namespace Cadenza.Local;

public class MusicDirectoryAccess : IMusicDirectory
{
    private readonly IOptions<MusicLibrary> _config;
    private readonly IFileAccess _fileAccess;

    public MusicDirectoryAccess(IOptions<MusicLibrary> config, IFileAccess fileAccess)
    {
        _config = config;
        _fileAccess = fileAccess;
    }

    public async Task<List<string>> GetAllFiles()
    {
        var files = await GetFiles();
        return files
            .Select(f => f.Path)
            .ToList();
    }

    public async Task<List<string>> GetModifiedFiles(DateTime sinceDate)
    {
        var files = await GetFiles();
        return files
            .Where(f => f.DateModified > sinceDate || f.DateCreated > sinceDate)
            .Select(f => f.Path)
            .ToList();
    }

    private async Task<IEnumerable<LocalFile>> GetFiles()
    {
        var directoryPath = _config.Value.Directory;
        var extensions = _config.Value.FileExtensions;

        return await _fileAccess.GetFiles(directoryPath, extensions);
    }
}