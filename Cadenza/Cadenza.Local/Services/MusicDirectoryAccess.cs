namespace Cadenza.Local;

public class MusicDirectoryAccess : IMusicDirectory
{
    private readonly IMusicDirectoryConfiguration _config;
    private readonly IFileAccess _fileAccess;

    public MusicDirectoryAccess(IMusicDirectoryConfiguration config, IFileAccess fileAccess)
    {
        _config = config;
        _fileAccess = fileAccess;
    }

    public List<string> GetAllFiles()
    {
        return GetFiles()
            .Select(f => f.Path)
            .ToList();
    }

    public List<string> GetModifiedFiles(DateTime sinceDate)
    {
        return GetFiles()
            .Where(f => f.DateModified > sinceDate)
            .Select(f => f.Path)
            .ToList();
    }

    private IEnumerable<LocalFile> GetFiles()
    {
        var directoryPath = _config.LibraryDirectoryPath;
        var extensions = _config.FileExtensions;

        return _fileAccess.GetFiles(directoryPath, extensions);
    }
}