namespace Cadenza.API.Core.Services.Cache;

internal class ArtistCache : IArtistCache
{
    private Dictionary<string, ArtistInfo> _artists;
    private Dictionary<string, List<AlbumInfo>> _albums;
    private List<string> _albumArtists;
    private List<string> _trackArtists;
    private Dictionary<Grouping, List<ArtistInfo>> _groupings;
    private Dictionary<string, List<ArtistInfo>> _genres;
    private Dictionary<string, List<TrackInfo>> _tracks;

    public Task<List<Artist>> GetAlbumArtists()
    {
        var result = _albumArtists
            .Select(a => _artists[a])
            .OfType<Artist>()
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<Album>> GetAlbums(string artistId)
    {
        var result = _albums.TryGetValue(artistId, out List<AlbumInfo> albums)
            ? albums.OfType<Album>().ToList()
            : new List<Album>();

        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetAllArtists()
    {
        var result = _artists
            .Values.OfType<Artist>().ToList()
            .ToList();

        return Task.FromResult(result);
    }

    public Task<ArtistInfo> GetArtist(string id)
    {
        var result = _artists.TryGetValue(id, out ArtistInfo artist)
            ? artist
            : null;

        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetArtistsByGenre(string id)
    {
        var artists = _genres.TryGetValue(id, out List<ArtistInfo> result)
            ? result.OfType<Artist>().ToList()
            : new List<Artist>();

        return Task.FromResult(artists);
    }

    public Task<List<Artist>> GetArtistsByGrouping(Grouping id)
    {
        var artists = _groupings.TryGetValue(id, out List<ArtistInfo> result)
            ? result.OfType<Artist>().ToList()
            : new List<Artist>();

        return Task.FromResult(artists);
    }

    public Task<List<Artist>> GetTrackArtists()
    {
        var result = _trackArtists
            .Select(a => _artists[a])
            .OfType<Artist>()
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<Track>> GetTracks(string artistId)
    {
        var result = _tracks.TryGetValue(artistId, out List<TrackInfo> tracks)
            ? tracks.OfType<Track>().ToList()
            : new List<Track>();

        return Task.FromResult(result);
    }

    public Task Populate(FullLibrary library)
    {
        _artists = library.Artists.ToDictionary(a => a.Id, a => a);
        _groupings = library.Artists.GroupBy(a => a.Grouping).ToDictionary(g => g.Key, g => g.ToList());
        _genres = library.Artists.GroupBy(a => a.Genre ?? "None").ToDictionary(g => g.Key, g => g.ToList());

        _albumArtists = library.Albums.Select(a => a.ArtistId).Distinct().ToList();
        _trackArtists = library.Tracks.Select(a => a.ArtistId).Distinct().ToList();

        _albums = _albumArtists.ToDictionary(a => a, a => new List<AlbumInfo>());
        _tracks = _trackArtists.ToDictionary(a => a, a => new List<TrackInfo>());

        foreach (var album in library.Albums)
        {
            if (album.ArtistId == null)
            {
                throw new Exception($"Artist ID is null for album {album.Id} ({album.Title})");
            }

            _albums[album.ArtistId].Add(album);
        }

        foreach (var track in library.Tracks)
        {
            if (track.ArtistId == null)
            {
                throw new Exception($"Artist ID is null for track {track.Id} ({track.Title})");
            }

            _tracks[track.ArtistId].Add(track);
        }

        return Task.CompletedTask;
    }

    public Task UpdateArtist(ArtistUpdate update)
    {
        if (!_artists.TryGetValue(update.Id, out ArtistInfo artist))
            return Task.CompletedTask;

        update.ApplyUpdates(artist);

        if (update.IsUpdated(ItemProperty.Genre, out PropertyUpdate genreUpdate))
        {
            var originalGenreArtists = _genres.GetOrAdd(genreUpdate.OriginalValue);
            var updatedGenreArtists = _genres.GetOrAdd(genreUpdate.UpdatedValue);

            originalGenreArtists.RemoveWhere(a => a.Id == update.Id);
            updatedGenreArtists.RemoveWhere(a => a.Id == update.Id);
            updatedGenreArtists.AddThenSort(update.UpdatedItem, a => a.Genre);
        }

        if (update.IsUpdated(ItemProperty.Grouping, out PropertyUpdate groupingUpdate))
        {
            var originalGrouping = groupingUpdate.OriginalValue.Parse<Grouping>();
            var updatedGrouping = groupingUpdate.UpdatedValue.Parse<Grouping>();

            var originalGroupingArtists = _groupings.GetOrAdd(originalGrouping);
            var updatedGroupingArtists = _groupings.GetOrAdd(updatedGrouping);

            originalGroupingArtists.RemoveWhere(a => a.Id == update.Id);
            updatedGroupingArtists.RemoveWhere(a => a.Id == update.Id);
            updatedGroupingArtists.AddThenSort(update.UpdatedItem, a => a.Genre);
        }

        return Task.CompletedTask;
    }
}
