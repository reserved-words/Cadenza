namespace Cadenza.API.Core.Services.Cache;

internal class Cache : ICache
{
    private CacheContainer _container;

    public Task Populate(FullLibrary library)
    {
        _container = new CacheContainer();
        _container.Populate(library);
        return Task.CompletedTask;
    }

    public Task<AlbumInfo> GetAlbum(string id)
    {
        var result = _container.Albums.GetValue(id);
        return Task.FromResult(result);
    }

    public Task<List<AlbumTrack>> GetAlbumTracks(string id)
    {
        var result = new List<AlbumTrack>();

        foreach (var track in _container.TracksByAlbum[id])
        {
            result.Add(new AlbumTrack(track, _container.AlbumTracks[track.Id]));
        }

        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetAlbumArtists()
    {
        var result = _container.AlbumsByArtist.Keys
            .Select(a => _container.Artists[a])
            .OfType<Artist>()
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<Album>> GetAlbums(string artistId)
    {
        var result = _container.AlbumsByArtist.GetList<string, AlbumInfo, Album>(artistId);
        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetAllArtists()
    {
        var result = _container.Artists.GetAllValues<string, ArtistInfo, Artist>();
        return Task.FromResult(result);
    }

    public Task<ArtistInfo> GetArtist(string id)
    {
        var result = _container.Artists.GetValue(id);
        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetArtistsByGenre(string id)
    {
        var artists = _container.ArtistsByGenre.TryGetValue(id, out List<ArtistInfo> result)
            ? result.OfType<Artist>().ToList()
            : new List<Artist>();

        return Task.FromResult(artists);
    }

    public Task<List<Artist>> GetArtistsByGrouping(Grouping id)
    {
        var artists = _container.ArtistsByGrouping.TryGetValue(id, out List<ArtistInfo> result)
            ? result.OfType<Artist>().ToList()
            : new List<Artist>();

        return Task.FromResult(artists);
    }

    public Task<List<Artist>> GetTrackArtists()
    {
        var result = _container.TracksByArtist.Keys
            .Select(a => _container.Artists[a])
            .OfType<Artist>()
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<Track>> GetArtistTracks(string artistId)
    {
        var result = _container.TracksByArtist.TryGetValue(artistId, out List<TrackInfo> tracks)
            ? tracks.OfType<Track>().ToList()
            : new List<Track>();

        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchAlbums()
    {
        var result = _container.Items[PlayerItemType.Album];
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchArtists()
    {
        var result = _container.Items[PlayerItemType.Artist];
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchTags()
    {
        var result = _container.Tags.Keys.Select(t => new SearchableTag(t)).OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchTracks()
    {
        var result = _container.Items[PlayerItemType.Track];
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchGenres()
    {
        var result = _container.Items[PlayerItemType.Genre];
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchGroupings()
    {
        var result = _container.Items[PlayerItemType.Grouping];
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetTag(string id)
    {
        var result = _container.Tags[id];
        return Task.FromResult(result);
    }
    public Task<TrackFull> GetTrack(string id)
    {
        if (!_container.Tracks.ContainsKey(id))
            return null;

        var track = _container.Tracks[id];
        var album = _container.Albums[track.AlbumId];

        var result = new TrackFull
        {
            Track = track,
            Album = album,
            AlbumTrack = _container.AlbumTracks[track.Id],
            Artist = _container.Artists[track.ArtistId],
            AlbumArtist = _container.Artists[album.ArtistId]
        };

        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayAll()
    {
        var result = _container.PlayTracks.Values.ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayAlbum(string id)
    {
        var result = _container.TracksByAlbum[id].Select(t => _container.PlayTracks[t.Id]).ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayArtist(string id)
    {
        var result = _container.TracksByArtist[id].Select(t => _container.PlayTracks[t.Id]).ToList();
        return Task.FromResult(result);
    }

    public async Task<List<PlayTrack>> PlayGenre(string id)
    {
        var result = new List<PlayTrack>();

        foreach (var artist in _container.ArtistsByGenre[id])
        {
            result.AddRange(await PlayArtist(artist.Id));
        }

        return result;
    }

    public async Task<List<PlayTrack>> PlayGrouping(Grouping id)
    {
        var result = new List<PlayTrack>();

        foreach (var artist in _container.ArtistsByGrouping[id])
        {
            result.AddRange(await PlayArtist(artist.Id));
        }

        return result;
    }

    public Task<List<PlayTrack>> PlayTag(string id)
    {
        var result = _container.TagPlayTracks[id];
        return Task.FromResult(result);
    }
}