namespace Cadenza.Local.API;

internal class AppConfiguration : ILibraryConfiguration
{
    private readonly IConfiguration _config;

    public AppConfiguration(IConfiguration config)
    {
        _config = config;
    }

    public string CurrentlyPlayingLocation => _config.GetValue<string>("CurrentlyPlayingLocation");
    public string CurrentlyPlayingPrefix => _config.GetValue<string>("CurrentlyPlayingPrefix");
    public List<string> FileExtensions => _config.GetSection("FileExtensions").Get<string[]>().ToList();
    public string UpdateQueueFilePath => _config.GetValue<string>("UpdateQueueFilePath");

    public string LibraryArtistsPath => _config.GetSection("LibraryPaths").GetValue<string>("Artists");
    public string LibraryAlbumsPath => _config.GetSection("LibraryPaths").GetValue<string>("Albums");
    public string LibraryTracksPath => _config.GetSection("LibraryPaths").GetValue<string>("Tracks");
    public string LibraryAlbumTrackLinksPath => _config.GetSection("LibraryPaths").GetValue<string>("AlbumTrackLinks");
    public string LibraryUpdatePath => _config.GetSection("LibraryPaths").GetValue<string>("Update");
}