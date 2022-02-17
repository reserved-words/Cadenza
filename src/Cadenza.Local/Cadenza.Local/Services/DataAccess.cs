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

    public async Task<List<JsonArtist>> GetArtists()
    {
        var path = GetPath(opt => opt.Artists);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonArtist>>(json);
    }

    public async Task<List<JsonAlbum>> GetAlbums()
    {
        var path = GetPath(opt => opt.Albums);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonAlbum>>(json);
    }

    public async Task<List<JsonTrack>> GetTracks()
    {
        var path = GetPath(opt => opt.Tracks);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonTrack>>(json);
    }

    public async Task<List<JsonAlbumTrackLink>> GetAlbumTrackLinks()
    {
        var path = GetPath(opt => opt.AlbumTrackLinks);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonAlbumTrackLink>>(json);
    }

    public async Task SaveArtists(List<JsonArtist> artists)
    {
        var path = GetPath(opt => opt.Artists);
        var json = _jsonConverter.Serialize(artists);
        await _fileAccess.SaveText(path, json);
    }

    public async Task SaveAlbums(List<JsonAlbum> albums)
    {
        var path = GetPath(opt => opt.Albums);
        var json = _jsonConverter.Serialize(albums);
        await _fileAccess.SaveText(path, json);
    }

    public async Task SaveTracks(List<JsonTrack> tracks)
    {
        var path = GetPath(opt => opt.Tracks);
        var json = _jsonConverter.Serialize(tracks);
        await _fileAccess.SaveText(path, json);
    }

    public async Task SaveAlbumTrackLinks(List<JsonAlbumTrackLink> albumTrackLinks)
    {
        var path = GetPath(opt => opt.AlbumTrackLinks);
        var json = _jsonConverter.Serialize(albumTrackLinks);
        await _fileAccess.SaveText(path, json);
    }

    public async Task<JsonItems> GetAll()
    {
        return new JsonItems
        {
            Tracks = await GetTracks(),
            Artists = await GetArtists(),
            Albums = await GetAlbums(),
            AlbumTrackLinks = await GetAlbumTrackLinks()
        };
    }

    public async Task SaveAll(JsonItems items)
    {
        // should be transaction
        await SaveTracks(items.Tracks);
        await SaveArtists(items.Artists);
        await SaveAlbums(items.Albums);
        await SaveAlbumTrackLinks(items.AlbumTrackLinks);
    }

    public async Task<JsonUpdateHistory> GetUpdateHistory()
    {
        var path = GetPath(opt => opt.UpdateHistory);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<JsonUpdateHistory>(json);
    }

    public async Task SaveUpdateHistory(JsonUpdateHistory history)
    {
        var path = GetPath(opt => opt.UpdateHistory);
        var json = _jsonConverter.Serialize(history);
        await _fileAccess.SaveText(path, json);
    }

    private string GetPath(Func<LibraryPaths, string> getFileName)
    {
        var directory = _config.Value.BaseDirectory;
        return Path.Combine(directory, getFileName(_config.Value));
    }
}