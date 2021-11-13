namespace Cadenza.Local;

public class PlayedFilesHandler : IPlayedFilesHandler
{
    private readonly IFileAccess _fileAccess;
    private readonly ILibraryConfiguration _config;

    public PlayedFilesHandler(IFileAccess fileAccess, ILibraryConfiguration config)
    {
        _fileAccess = fileAccess;
        _config = config;
    }

    public void RemovePlayedFiles()
    {
        var files = _fileAccess.GetFiles(_config.CurrentlyPlayingLocation, _config.FileExtensions);

        var allButMostRecentFiles = files
            .Where(f => Path.GetFileName(f.Path).StartsWith(_config.CurrentlyPlayingPrefix))
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