using Microsoft.Extensions.Logging;

namespace Cadenza.Local.SyncService.Services;

internal class PlayedFilesService : IService
{
    private readonly IFileAccess _fileAccess;
    private readonly ILogger<PlayedFilesService> _logger;
    private readonly IOptions<CurrentlyPlayingSettings> _settings;

    public PlayedFilesService(IFileAccess fileAccess, IOptions<CurrentlyPlayingSettings> settings, ILogger<PlayedFilesService> logger)
    {
        _fileAccess = fileAccess;
        _settings = settings;
        _logger = logger;
    }

    public Task Run()
    {
        _logger.LogInformation($"Checking for played files");

        var files = GetPlayedFiles();

        foreach (var file in files)
        {
            TryDeleteFile(file.Path);
        }

        _logger.LogInformation($"Played files processed");

        return Task.CompletedTask;
    }

    private List<FileDetails> GetPlayedFiles()
    {
        var directory = Path.Combine(_settings.Value.BaseDirectory, _settings.Value.DirectoryName);

        var allFiles = _fileAccess.GetFiles(directory);

        _logger.LogInformation($"{allFiles.Count} total files found");

        var allButLatestFile = allFiles
            .Where(f => Path.GetFileName(f.Path).StartsWith(_settings.Value.FilePrefix))
            .OrderByDescending(f => f.DateCreated)
            .Skip(1)
            .ToList();

        _logger.LogInformation($"{allButLatestFile.Count} files to be deleted");

        return allButLatestFile;
    }

    private void TryDeleteFile(string path)
    {
        try
        {
            _fileAccess.DeleteFile(path);
        }
        catch (IOException ex)
        {
            _logger.LogWarning(ex, "File could not be deleted at this time");
        }
    }
}