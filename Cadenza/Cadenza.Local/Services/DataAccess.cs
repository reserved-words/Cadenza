namespace Cadenza.Local;

public class DataAccess : IDataAccess
{
    private readonly IFileAccess _fileAccess;
    private readonly IJsonConverter _jsonConverter;
    private readonly ILibraryConfiguration _config;

    public DataAccess(IFileAccess fileAccess, IJsonConverter jsonConverter, ILibraryConfiguration config)
    {
        _config = config;
        _fileAccess = fileAccess;
        _jsonConverter = jsonConverter;
    }

    public List<JsonArtist> GetArtists()
    {
        var path = _config.LibraryArtistsPath;
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonArtist>>(json);
    }

    public List<JsonAlbum> GetAlbums()
    {
        var path = _config.LibraryAlbumsPath;
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonAlbum>>(json);
    }

    public List<JsonTrack> GetTracks()
    {
        var path = _config.LibraryTracksPath;
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonTrack>>(json);
    }

    public List<JsonAlbumTrackLink> GetAlbumTrackLinks()
    {
        var path = _config.LibraryAlbumTrackLinksPath;
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonAlbumTrackLink>>(json);
    }

    public void SaveArtists(List<JsonArtist> artists)
    {
        var path = _config.LibraryArtistsPath;
        var json = _jsonConverter.Serialize(artists);
        _fileAccess.SaveText(path, json);
    }

    public void SaveAlbums(List<JsonAlbum> albums)
    {
        var path = _config.LibraryAlbumsPath;
        var json = _jsonConverter.Serialize(albums);
        _fileAccess.SaveText(path, json);
    }

    public void SaveTracks(List<JsonTrack> tracks)
    {
        var path = _config.LibraryTracksPath;
        var json = _jsonConverter.Serialize(tracks);
        _fileAccess.SaveText(path, json);
    }

    public void SaveAlbumTrackLinks(List<JsonAlbumTrackLink> albumTrackLinks)
    {
        var path = _config.LibraryAlbumTrackLinksPath;
        var json = _jsonConverter.Serialize(albumTrackLinks);
        _fileAccess.SaveText(path, json);
    }

    public JsonItems GetAll()
    {
        return new JsonItems
        {
            Tracks = GetTracks(),
            Artists = GetArtists(),
            Albums = GetAlbums(),
            AlbumTrackLinks = GetAlbumTrackLinks()
        };
    }

    public void SaveAll(JsonItems items)
    {
        // should be transaction
        SaveTracks(items.Tracks);
        SaveArtists(items.Artists);
        SaveAlbums(items.Albums);
        SaveAlbumTrackLinks(items.AlbumTrackLinks);
    }

    public JsonUpdateHistory GetUpdateHistory()
    {
        var path = _config.LibraryUpdatePath;
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<JsonUpdateHistory>(json);
    }

    public void SaveUpdateHistory(JsonUpdateHistory history)
    {
        var path = _config.LibraryUpdatePath;
        var json = _jsonConverter.Serialize(history);
        _fileAccess.SaveText(path, json);
    }
}