namespace Cadenza.Local.SyncService;

internal class ServiceConfiguration : ILibraryConfiguration, IMusicDirectoryConfiguration
{
    private readonly IConfiguration _config;

    public ServiceConfiguration(IConfiguration config)
    {
        _config = config;
    }

    public string LibraryDirectoryPath => _config.GetValue<string>("LibraryPath");
    public string CurrentlyPlayingLocation => _config.GetValue<string>("CurrentlyPlayingLocation");
    public List<string> FileExtensions => _fileExtensions.Get<string[]>().ToList();
    public string CurrentlyPlayingPrefix => _config.GetValue<string>("CurrentlyPlayingPrefix");
    public string UpdateQueueFilePath => _config.GetValue<string>("UpdateQueueFilePath");

    public string LibraryArtistsPath => _libraryPaths.GetValue<string>("Artists");
    public string LibraryAlbumsPath => _libraryPaths.GetValue<string>("Albums");
    public string LibraryTracksPath => _libraryPaths.GetValue<string>("Tracks");
    public string LibraryAlbumTrackLinksPath => _libraryPaths.GetValue<string>("AlbumTrackLinks");
    public string LibraryUpdatePath => _libraryPaths.GetValue<string>("Update");

    private IConfigurationSection _fileExtensions => _config.GetSection("FileExtensions");
    private IConfigurationSection _libraryPaths => _config.GetSection("LibraryPaths");
}