using Cadenza.Domain.Model;
using Cadenza.Local.API.Core.Interfaces;
using Cadenza.Local.API.Core.Settings;
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

    public Task<List<FileDetails>> GetAllFiles()
    {
        var allFiles = GetFiles().ToList();
        return Task.FromResult(allFiles);
    }

    public Task<List<FileDetails>> GetModifiedFiles(DateTime sinceDate)
    {
        var allFiles = GetFiles();
        var modifiedFiles = allFiles
            .Where(f => f.DateModified > sinceDate)
            .ToList();
        return Task.FromResult(modifiedFiles);
    }

    private IEnumerable<FileDetails> GetFiles()
    {
        var directoryPath = _config.Value.Directory;
        var extensions = _config.Value.FileExtensions;

        return _fileAccess.GetFiles(directoryPath, extensions);
    }
}