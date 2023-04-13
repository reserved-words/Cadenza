using Microsoft.Extensions.Options;

namespace Cadenza.Local.API.Core.Services;

internal class MusicDirectory : IMusicDirectory
{
    private readonly MusicLibrarySettings _settings;
    private readonly IFileAccess _fileAccess;

    public MusicDirectory(IOptions<MusicLibrarySettings> settings, IFileAccess fileAccess)
    {
        _settings = settings.Value;
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

    public Task RemoveFile(string filepath)
    {
        var filename = Path.GetFileName(filepath);
        var targetLocation = Path.Combine(_settings.RemovedDirectory, filename);
        _fileAccess.MoveFile(filepath, targetLocation);
        return Task.CompletedTask;
    }

    private IEnumerable<FileDetails> GetFiles()
    {
        var directoryPath = _settings.Directory;
        var extensions = _settings.FileExtensions;
        return _fileAccess.GetFiles(directoryPath, extensions);
    }
}