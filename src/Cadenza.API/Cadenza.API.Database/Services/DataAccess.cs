using Cadenza.Domain.Model.Updates;

namespace Cadenza.API.Database.Services;

internal class DataAccess : IDataAccess
{
    private readonly IFilePathService _paths;
    private readonly IFileDataService _service;

    public DataAccess(IFileDataService service, IFilePathService paths)
    {
        _service = service;
        _paths = paths;
    }

    public async Task<JsonItems> GetAll(LibrarySource? source)
    {
        var library = new JsonItems
        {
            Artists = await GetArtists(),
            Albums = new List<JsonAlbum>(),
            Tracks = new List<JsonTrack>(),
            AlbumTracks = new List<JsonAlbumTrack>()
        };

        if (source.HasValue)
        {
            await AddSource(library, source.Value);
        }
        else
        {
            foreach (var src in Enum.GetValues<LibrarySource>())
            {
                await AddSource(library, src);
            }
        }
        
        return library;
    }

    private async Task AddSource(JsonItems library, LibrarySource source)
    {
        library.Albums.AddRange(await GetAlbums(source));
        library.Tracks.AddRange(await GetTracks(source));
        library.AlbumTracks.AddRange(await GetAlbumTracks(source));

        // Needed for now because Source property was added later - as soon as done a save in each environment can remove this
        foreach (var album in library.Albums)
        {
            album.Source = source;
        }
    }

    public async Task<List<JsonArtist>> GetArtists()
    {
        return await _service.Get<List<JsonArtist>>(_paths.Artists());
    }

    public async Task<List<JsonAlbum>> GetAlbums(LibrarySource source)
    {
        return await _service.Get<List<JsonAlbum>>(_paths.Albums(source));
    }

    public async Task<List<JsonTrack>> GetTracks(LibrarySource source)
    {
        return await _service.Get<List<JsonTrack>>(_paths.Tracks(source));
    }

    public async Task<List<JsonAlbumTrack>> GetAlbumTracks(LibrarySource source)
    {
        return await _service.Get<List<JsonAlbumTrack>>(_paths.AlbumTracks(source));
    }

    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        return await _service.Get<List<ItemUpdates>>(_paths.Updates(source));
    }

    public async Task UpdateLibrary(LibrarySource source, Action<JsonItems> action)
    {
        var library = await GetAll(source);
        action(library);
        await SaveAll(library, source);
    }

    public async Task UpdateUpdates(LibrarySource source, Action<List<ItemUpdates>> action)
    {
        var updates = await GetUpdates(source);
        action(updates);
        await SaveUpdates(updates, source);
    }

    private async Task SaveAlbums(LibrarySource source, List<JsonAlbum> albums)
    {
        await _service.Save(_paths.Albums(source), albums);
    }

    private async Task SaveAlbumTracks(LibrarySource source, List<JsonAlbumTrack> albumTracks)
    {
        await _service.Save(_paths.AlbumTracks(source), albumTracks);
    }

    private async Task SaveArtists(List<JsonArtist> artists)
    {
        await _service.Save(_paths.Artists(), artists);
    }

    private async Task SaveTracks(LibrarySource source, List<JsonTrack> tracks)
    {
        await _service.Save(_paths.Tracks(source), tracks);
    }

    private async Task SaveAll(JsonItems library, LibrarySource source)
    {
        await SaveTracks(source, library.Tracks);
        await SaveArtists(library.Artists);
        await SaveAlbums(source, library.Albums);
        await SaveAlbumTracks(source, library.AlbumTracks);
    }

    private async Task SaveUpdates(List<ItemUpdates> updates, LibrarySource source)
    {
        await _service.Save(_paths.Updates(source), updates);
    }

    public async Task UpdateAlbum(LibrarySource source, string id, Action<JsonAlbum> update)
    {
        var albums = await GetAlbums(source);

        var album = albums.SingleOrDefault(a => a.Id == id);

        if (album == null)
            return;

        update(album);

        await SaveAlbums(source, albums);
    }

    public async Task UpdateArtist(string id, Action<JsonArtist> update)
    {
        var artists = await GetArtists();

        var artist = artists.SingleOrDefault(a => a.Id == id);

        if (artist == null)
            return;

        update(artist);

        await SaveArtists(artists);
    }

    public async Task UpdateTrack(LibrarySource source, string id, Action<JsonTrack> update)
    {
        var tracks = await GetTracks(source);

        var track = tracks.SingleOrDefault(a => a.Id == id);

        if (track == null)
            return;

        update(track);

        await SaveTracks(source, tracks);
    }
}