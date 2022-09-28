using Microsoft.Extensions.Options;

namespace Cadenza.Local.SyncService.Services;

internal class PlayedFilesCleanupService : IUpdateService
{
    private readonly IFileAccess _fileAccess;
    private readonly IOptions<CurrentlyPlayingSettings> _settings;

    public PlayedFilesCleanupService(IFileAccess fileAccess, IOptions<CurrentlyPlayingSettings> settings)
    {
        _fileAccess = fileAccess;
        _settings = settings;
    }

    public Task Run()
    {
        var files = GetPlayedFiles();

        foreach (var file in files)
        {
            TryDeleteFile(file.Path);
        }

        return Task.CompletedTask;
    }

    private IEnumerable<FileDetails> GetPlayedFiles()
    {
        var directory = Path.Combine(_settings.Value.BaseDirectory, _settings.Value.DirectoryName);

        var allFiles = _fileAccess.GetFiles(directory);

        var allButLatestFile = allFiles
            .Where(f => Path.GetFileName(f.Path).StartsWith(_settings.Value.FilePrefix))
            .OrderByDescending(f => f.DateCreated)
            .Skip(1);

        return allButLatestFile;
    }

    private void TryDeleteFile(string path)
    {
        try
        {
            _fileAccess.DeleteFile(path);
        }
        catch (IOException)
        {
            // skip - file is in use
        }
    }
}