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
        var directoryPath = _config.Value.Directory;
        var extensions = _config.Value.FileExtensions;

        return _fileAccess.GetFiles(directoryPath, extensions);
    }
}