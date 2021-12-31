namespace Cadenza.Local.API;

internal class AppConfiguration : ILibraryConfiguration
{
    private readonly IConfiguration _config;

    public AppConfiguration(IConfiguration config)
    {
        _config = config;
    }

    private string BaseDirectory => _config.GetValue<string>("BaseDirectory");

    public string CurrentlyPlayingLocation => GetPath("CurrentlyPlayingLocation");
    public string CurrentlyPlayingPrefix => _config.GetValue<string>("CurrentlyPlayingPrefix");
    public List<string> FileExtensions => _config.GetSection("FileExtensions").Get<string[]>().ToList();
    public string UpdateQueueFilePath => GetPath("UpdateQueueFilePath");

    public string LibraryArtistsPath => GetLibraryPath("Artists");
    public string LibraryAlbumsPath => GetLibraryPath("Albums");
    public string LibraryTracksPath => GetLibraryPath("Tracks");
    public string LibraryAlbumTrackLinksPath => GetLibraryPath("AlbumTrackLinks");
    public string LibraryUpdatePath => GetLibraryPath("Update");

    private string GetPath(string key)
    {
        var name = _config.GetValue<string>(key);
        return Path.Combine(BaseDirectory, name);
    }

    private string GetLibraryPath(string key)
    {
        var name = _config.GetSection("LibraryPaths").GetValue<string>(key);
        return Path.Combine(BaseDirectory, name);
    }
}