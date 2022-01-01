using Microsoft.Extensions.Options;

namespace Cadenza.Local;

public class DataAccess : IDataAccess
{
    private readonly IFileAccess _fileAccess;
    private readonly IJsonConverter _jsonConverter;
    private readonly IOptions<LibraryPaths> _config;

    public DataAccess(IFileAccess fileAccess, IJsonConverter jsonConverter, IOptions<LibraryPaths> config)
    {
        _config = config;
        _fileAccess = fileAccess;
        _jsonConverter = jsonConverter;
    }

    public List<JsonArtist> GetArtists()
    {
        var path = GetPath(opt => opt.Artists);
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonArtist>>(json);
    }

    public List<JsonAlbum> GetAlbums()
    {
        var path = GetPath(opt => opt.Albums);
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonAlbum>>(json);
    }

    public List<JsonTrack> GetTracks()
    {
        var path = GetPath(opt => opt.Tracks);
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonTrack>>(json);
    }

    public List<JsonAlbumTrackLink> GetAlbumTrackLinks()
    {
        var path = GetPath(opt => opt.AlbumTrackLinks);
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonAlbumTrackLink>>(json);
    }

    public void SaveArtists(List<JsonArtist> artists)
    {
        var path = GetPath(opt => opt.Artists);
        var json = _jsonConverter.Serialize(artists);
        _fileAccess.SaveText(path, json);
    }

    public void SaveAlbums(List<JsonAlbum> albums)
    {
        var path = GetPath(opt => opt.Albums);
        var json = _jsonConverter.Serialize(albums);
        _fileAccess.SaveText(path, json);
    }

    public void SaveTracks(List<JsonTrack> tracks)
    {
        var path = GetPath(opt => opt.Tracks);
        var json = _jsonConverter.Serialize(tracks);
        _fileAccess.SaveText(path, json);
    }

    public void SaveAlbumTrackLinks(List<JsonAlbumTrackLink> albumTrackLinks)
    {
        var path = GetPath(opt => opt.AlbumTrackLinks);
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
        var path = GetPath(opt => opt.UpdateHistory);
        var json = _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<JsonUpdateHistory>(json);
    }

    public void SaveUpdateHistory(JsonUpdateHistory history)
    {
        var path = GetPath(opt => opt.UpdateHistory);
        var json = _jsonConverter.Serialize(history);
        _fileAccess.SaveText(path, json);
    }

    private string GetPath(Func<LibraryPaths, string> getFileName)
    {
        var directory = _config.Value.BaseDirectory;
        return Path.Combine(directory, getFileName(_config.Value));
    }
}