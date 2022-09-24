using Microsoft.Extensions.Options;
using Cadenza.Domain;
using Cadenza.Utilities;
using Cadenza.API.Database.Model;
using Cadenza.API.Database.Interfaces;

namespace Cadenza.API.Database.Services;

internal class DataAccess : IDataAccess
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
        var path = GetPath(opt => opt.Artists, null);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonArtist>>(json);
    }

    public async Task<List<JsonAlbum>> GetAlbums(LibrarySource source)
    {
        var path = GetPath(opt => opt.Albums, source);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonAlbum>>(json);
    }

    public async Task<List<JsonTrack>> GetTracks(LibrarySource source)
    {
        var path = GetPath(opt => opt.Tracks, source);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonTrack>>(json);
    }

    public async Task<List<JsonAlbumTrackLink>> GetAlbumTrackLinks(LibrarySource source)
    {
        var path = GetPath(opt => opt.AlbumTrackLinks, source);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<List<JsonAlbumTrackLink>>(json);
    }

    public async Task SaveArtists(List<JsonArtist> artists)
    {
        var path = GetPath(opt => opt.Artists, null);
        var json = _jsonConverter.Serialize(artists);
        await _fileAccess.SaveText(path, json);
    }

    public async Task SaveAlbums(List<JsonAlbum> albums, LibrarySource source)
    {
        var path = GetPath(opt => opt.Albums, source);
        var json = _jsonConverter.Serialize(albums);
        await _fileAccess.SaveText(path, json);
    }

    public async Task SaveTracks(List<JsonTrack> tracks, LibrarySource source)
    {
        var path = GetPath(opt => opt.Tracks, source);
        var json = _jsonConverter.Serialize(tracks);
        await _fileAccess.SaveText(path, json);
    }

    public async Task SaveAlbumTrackLinks(List<JsonAlbumTrackLink> albumTrackLinks, LibrarySource source)
    {
        var path = GetPath(opt => opt.AlbumTrackLinks, source);
        var json = _jsonConverter.Serialize(albumTrackLinks);
        await _fileAccess.SaveText(path, json);
    }

    public async Task<JsonItems> GetAll(LibrarySource source)
    {
        return new JsonItems
        {
            Tracks = await GetTracks(source),
            Artists = await GetArtists(),
            Albums = await GetAlbums(source),
            AlbumTrackLinks = await GetAlbumTrackLinks(source)
        };
    }

    public async Task SaveAll(JsonItems items, LibrarySource source)
    {
        // should be transaction
        await SaveTracks(items.Tracks, source);
        await SaveArtists(items.Artists);
        await SaveAlbums(items.Albums, source);
        await SaveAlbumTrackLinks(items.AlbumTrackLinks, source);
    }

    public async Task<JsonUpdateHistory> GetUpdateHistory(LibrarySource source)
    {
        var path = GetPath(opt => opt.UpdateHistory, source);
        var json = await _fileAccess.GetText(path);
        return _jsonConverter.Deserialize<JsonUpdateHistory>(json);
    }

    public async Task SaveUpdateHistory(JsonUpdateHistory history, LibrarySource source)
    {
        var path = GetPath(opt => opt.UpdateHistory, source);
        var json = _jsonConverter.Serialize(history);
        await _fileAccess.SaveText(path, json);
    }

    private string GetPath(Func<LibraryPaths, string> getFileName, LibrarySource? source)
    {
        var directory = _config.Value.BaseDirectory;
        return source.HasValue
            ? Path.Combine(directory, source.Value.ToString(), getFileName(_config.Value))
            : Path.Combine(directory, getFileName(_config.Value));
    }
}