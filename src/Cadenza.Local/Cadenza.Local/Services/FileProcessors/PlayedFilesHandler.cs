using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.FileProcessors;
using Microsoft.Extensions.Options;

namespace Cadenza.Local;

public class PlayedFilesHandler : IPlayedFilesHandler
{
    private readonly IFileAccess _fileAccess;
    private readonly IOptions<CurrentlyPlaying> _currentlyPlaying;
    private readonly IOptions<MusicLibrary> _musicLibrary;

    public PlayedFilesHandler(IFileAccess fileAccess, IOptions<CurrentlyPlaying> currentlyPlaying, IOptions<MusicLibrary> musicLibrary)
    {
        _fileAccess = fileAccess;
        _currentlyPlaying = currentlyPlaying;
        _musicLibrary = musicLibrary;
    }

    public async Task RemovePlayedFiles()
    {
        var directory = Path.Combine(_currentlyPlaying.Value.BaseDirectory, _currentlyPlaying.Value.DirectoryName);

        var files = await _fileAccess.GetFiles(directory, _musicLibrary.Value.FileExtensions);

        var allButMostRecentFiles = files
            .Where(f => Path.GetFileName(f.Path).StartsWith(_currentlyPlaying.Value.FilePrefix))
            .OrderByDescending(f => f.DateCreated)
            .Skip(2);

        foreach (var file in allButMostRecentFiles)
        {
            TryDeleteFile(file.Path);
        }
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