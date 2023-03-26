using Cadenza.API.JsonLibrary.Interfaces;

namespace Cadenza.API.JsonLibrary.Services;

internal class DataAccess : IDataAccess
{
    private readonly IFilePathService _paths;
    private readonly IFileDataService _service;

    public DataAccess(IFileDataService service, IFilePathService paths)
    {
        _service = service;
        _paths = paths;
    }

    public async Task<FullLibrary> GetAll(LibrarySource? source)
    {
        var library = new FullLibrary
        {
            Artists = await GetArtists(),
            Albums = new List<AlbumInfo>(),
            Tracks = new List<TrackInfo>(),
            AlbumTracks = new List<AlbumTrackLink>()
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

    private async Task AddSource(FullLibrary library, LibrarySource source)
    {
        library.Albums.AddRange(await GetAlbums(source));
        library.Tracks.AddRange(await GetTracks(source));
        library.AlbumTracks.AddRange(await GetAlbumTracks(source));
    }

    public async Task<List<ArtistInfo>> GetArtists()
    {
        return await _service.Get<List<ArtistInfo>>(_paths.Artists());
    }

    public async Task<List<AlbumInfo>> GetAlbums(LibrarySource source)
    {
        var albums = await _service.Get<List<AlbumInfo>>(_paths.Albums(source));

        // Temporarily need to populate artist names - once done a few saves can remove this
        var artistNames = (await GetArtists()).ToDictionary(a => a.Id, a => a.Name);
        foreach (var album in albums)
        {
            album.ArtistName = artistNames[album.ArtistId];
        }
        // End temporary code

        return albums;
    }

    public async Task<List<TrackInfo>> GetTracks(LibrarySource source)
    {
        var tracks = await _service.Get<List<TrackInfo>>(_paths.Tracks(source));

        // Temporarily need to populate artist names and track years - once done a few saves can remove this
        var artistNames = (await GetArtists()).ToDictionary(a => a.Id, a => a.Name);
        var albumYears = (await GetAlbums(source)).ToDictionary(a => a.Id, a => a.Year);
        foreach (var track in tracks)
        {
            track.ArtistName = artistNames[track.ArtistId];
            if (string.IsNullOrWhiteSpace(track.Year))
            {
                track.Year = albumYears[track.AlbumId];
            }
        }
        // End temporary code

        return tracks;
    }

    public async Task<List<AlbumTrackLink>> GetAlbumTracks(LibrarySource source)
    {
        var albumTracks = await _service.Get<List<AlbumTrackLink>>(_paths.AlbumTracks(source));

        // Temporarily need to populate artist names - once done a few saves can remove this
        foreach (var albumTrack in albumTracks)
        {
            if (albumTrack.DiscNo == 0)
            {
                albumTrack.DiscNo = 1;
            }
        }
        // End temporary code

        return albumTracks;
    }

    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        return await _service.Get<List<ItemUpdates>>(_paths.Updates(source));
    }

    public async Task UpdateLibrary(LibrarySource source, Action<FullLibrary> action)
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

    private async Task SaveAlbums(LibrarySource source, List<AlbumInfo> albums)
    {
        await _service.Save(_paths.Albums(source), albums);
    }

    private async Task SaveAlbumTracks(LibrarySource source, List<AlbumTrackLink> albumTracks)
    {
        await _service.Save(_paths.AlbumTracks(source), albumTracks);
    }

    private async Task SaveArtists(List<ArtistInfo> artists)
    {
        await _service.Save(_paths.Artists(), artists);
    }

    private async Task SaveTracks(LibrarySource source, List<TrackInfo> tracks)
    {
        await _service.Save(_paths.Tracks(source), tracks);
    }

    private async Task SaveAll(FullLibrary library, LibrarySource source)
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

    public async Task UpdateAlbum(LibrarySource source, string id, Action<AlbumInfo> update)
    {
        var albums = await GetAlbums(source);

        var album = albums.SingleOrDefault(a => a.Id == id);

        if (album == null)
            return;

        update(album);

        await SaveAlbums(source, albums);
    }

    public async Task UpdateArtist(string id, Action<ArtistInfo> update)
    {
        var artists = await GetArtists();

        var artist = artists.SingleOrDefault(a => a.Id == id);

        if (artist == null)
            return;

        update(artist);

        await SaveArtists(artists);
    }

    public async Task UpdateTrack(LibrarySource source, string id, Action<TrackInfo> update)
    {
        var tracks = await GetTracks(source);

        var track = tracks.SingleOrDefault(a => a.Id == id);

        if (track == null)
            return;

        update(track);

        await SaveTracks(source, tracks);
    }
}