using Cadenza.API.JsonLibrary.Interfaces;

namespace Cadenza.API.JsonLibrary.Services;

internal class FilePathService : IFilePathService
{
    private readonly IOptions<LibraryPathSettings> _config;

    public FilePathService(IOptions<LibraryPathSettings> config)
    {
        _config = config;
    }

    public string Albums(LibrarySource source)
    {
        return GetPath(s => s.Albums, source);
    }

    public string AlbumTracks(LibrarySource source)
    {
        return GetPath(s => s.AlbumTrackLinks, source);
    }

    public string Artists()
    {
        return GetPath(s => s.Artists, null);
    }

    public string Tracks(LibrarySource source)
    {
        return GetPath(s => s.Tracks, source);
    }

    public string Updates(LibrarySource source)
    {
        return GetPath(s => s.UpdateQueue, source);
    }

    private string GetPath(Func<LibraryPathSettings, string> getFileName, LibrarySource? source)
    {
        var directory = _config.Value.BaseDirectory;
        return source.HasValue
            ? Path.Combine(directory, source.Value.ToString(), getFileName(_config.Value))
            : Path.Combine(directory, getFileName(_config.Value));
    }
}
