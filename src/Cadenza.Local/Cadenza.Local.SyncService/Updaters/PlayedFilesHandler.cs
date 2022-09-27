using Cadenza.Local.Common.Config;
using Cadenza.Utilities.Interfaces;
using Microsoft.Extensions.Options;

namespace Cadenza.Local.SyncService.Updaters;

internal class PlayedFilesHandler : IUpdateService
{
    private readonly IFileAccess _fileAccess;
    private readonly IOptions<CurrentlyPlayingSettings> _config;

    public PlayedFilesHandler(IFileAccess fileAccess, IOptions<CurrentlyPlayingSettings> config)
    {
        _fileAccess = fileAccess;
        _config = config;
    }

    public Task Run()
    {
        var directory = Path.Combine(_config.Value.BaseDirectory, _config.Value.DirectoryName);

        var allFiles = _fileAccess.GetFiles(directory);

        var allButLatestFile = allFiles
            .Where(f => Path.GetFileName(f.Path).StartsWith(_config.Value.FilePrefix))
            .OrderByDescending(f => f.DateCreated)
            .Skip(1);

        foreach (var file in allButLatestFile)
        {
            TryDeleteFile(file.Path);
        }

        return Task.CompletedTask;
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