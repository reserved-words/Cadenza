namespace Cadenza.Local.SyncService;

internal class ServiceConfiguration : ILibraryConfiguration, IMusicDirectoryConfiguration
{
    private readonly IConfiguration _config;

    public ServiceConfiguration(IConfiguration config)
    {
        _config = config;
    }

    private string BaseDirectory => _config.GetValue<string>("BaseDirectory");

    public string LibraryDirectoryPath => _config.GetValue<string>("LibraryPath");
    public string CurrentlyPlayingLocation => GetPath("CurrentlyPlayingLocation");
    public List<string> FileExtensions => _fileExtensions.Get<string[]>().ToList();
    public string CurrentlyPlayingPrefix => _config.GetValue<string>("CurrentlyPlayingPrefix");
    public string UpdateQueueFilePath => GetPath("UpdateQueueFilePath");

    public string LibraryArtistsPath => GetLibraryPath("Artists");
    public string LibraryAlbumsPath => GetLibraryPath("Albums");
    public string LibraryTracksPath => GetLibraryPath("Tracks");
    public string LibraryAlbumTrackLinksPath => GetLibraryPath("AlbumTrackLinks");
    public string LibraryUpdatePath => GetLibraryPath("Update");

    private IConfigurationSection _fileExtensions => _config.GetSection("FileExtensions");
    private IConfigurationSection _libraryPaths => _config.GetSection("LibraryPaths");

    private string GetPath(string key)
    {
        var name = _config.GetValue<string>(key);
        return Path.Combine(BaseDirectory, name);
    }

    private string GetLibraryPath(string key)
    {
        var name = _libraryPaths.GetValue<string>(key);
        return Path.Combine(BaseDirectory, name);
    }
}