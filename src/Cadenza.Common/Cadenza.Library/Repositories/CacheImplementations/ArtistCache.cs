using Newtonsoft.Json;

namespace Cadenza.Library;

public class ArtistCache : IArtistCache
{
    private Dictionary<string, ArtistInfo> _artists;
    private Dictionary<string, List<AlbumInfo>> _albums;
    private List<string> _albumArtists;
    private List<string> _trackArtists;
    private Dictionary<Grouping, List<ArtistInfo>> _groupings;
    private Dictionary<string, List<ArtistInfo>> _genres;

    public Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit)
    {
        var result = _albumArtists
            .Select(a => _artists[a])
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);

        return Task.FromResult(result);
    }

    public Task<ListResponse<Album>> GetAlbums(string artistId, int page, int limit)
    {
        var result = _albums.TryGetValue(artistId, out List<AlbumInfo> albums)
            ? albums.ToListResponse<Album>(a => a.Id, page, limit)
            : ListResponse<Album>.Empty;

        return Task.FromResult(result);
    }

    public Task<ListResponse<Artist>> GetAllArtists(int page, int limit)
    {
        var result = _artists
            .Values
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);

        return Task.FromResult(result);
    }

    public Task<ArtistInfo> GetArtist(string id)
    {
        var result = _artists.TryGetValue(id, out ArtistInfo artist)
            ? artist
            : null;

        return Task.FromResult(result);
    }

    public Task<ListResponse<Artist>> GetArtistsByGenre(string id, int page, int limit)
    {
        var artists = _genres.TryGetValue(id, out List<ArtistInfo> result)
            ? result.ToListResponse<Artist>(a => a.Id, page, limit)
            : ListResponse<Artist>.Empty;

        return Task.FromResult(artists);
    }

    public Task<ListResponse<Artist>> GetArtistsByGrouping(Grouping id, int page, int limit)
    {
        var artists = _groupings.TryGetValue(id, out List<ArtistInfo> result)
            ? result.ToListResponse<Artist>(a => a.Id, page, limit)
            : ListResponse<Artist>.Empty;

        return Task.FromResult(artists);
    }

    public Task<ListResponse<Artist>> GetTrackArtists(int page, int limit)
    {
        var result = _trackArtists
            .Select(a => _artists[a])
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);

        return Task.FromResult(result);
    }

    public async Task Populate(FullLibrary library)
    {
        _artists = library.Artists.ToDictionary(a => a.Id, a => a);
        _groupings = library.Artists.GroupBy(a => a.Grouping).ToDictionary(g => g.Key, g => g.ToList());
        _genres = library.Artists.GroupBy(a => a.Genre ?? "None").ToDictionary(g => g.Key, g => g.ToList());

        _albums = library.Artists.ToDictionary(a => a.Id, a => new List<AlbumInfo>());

        _albumArtists = library.Albums.Select(a => a.ArtistId).Distinct().ToList();
        _trackArtists = library.Tracks.Select(a => a.ArtistId).Distinct().ToList();

        foreach (var album in library.Albums)
        {
            if (album.ArtistId == null)
            {
                var json = JsonConvert.SerializeObject(album);
                var ex = new Exception($"Artist ID is null for {album.Id} ({album.Title})");
                ex.Data.Add("Album", json);
                throw ex;
            }

            if (!_albums.ContainsKey(album.ArtistId))
            {
                var exception = new Exception($"Artist {album.ArtistId} was not found in the albums dictionary");
                exception.Data.Add("AlbumsKeys", string.Join(",", _albums.Keys));
                throw exception;
            }

            if (_albums[album.ArtistId] == null)
            {
                throw new Exception($"Album list is null for {album.ArtistId}");
            }

            _albums[album.ArtistId].Add(album);
        }
    }

    public Task UpdateArtist(ArtistUpdate update)
    {
        if (!_artists.TryGetValue(update.Id, out ArtistInfo artist))
            return Task.CompletedTask;

        update.ApplyUpdates(artist);

        if (update.IsUpdated(ItemProperty.Genre, out ItemPropertyUpdate genreUpdate))
        {
            if (_genres.ContainsKey(genreUpdate.OriginalValue))
            {
                _genres[genreUpdate.OriginalValue].RemoveWhere(a => a.Id == update.Id);
            }
            var genreArtists = _genres.GetOrAdd(genreUpdate.UpdatedValue);
            genreArtists.RemoveWhere(a => a.Id == update.Id);
            genreArtists.AddThenSort(update.UpdatedItem, a => a.Genre);
        }

        if (update.IsUpdated(ItemProperty.Grouping, out ItemPropertyUpdate groupingUpdate))
        {
            var originalGrouping = groupingUpdate.OriginalValue.Parse<Grouping>();
            var updatedGrouping = groupingUpdate.UpdatedValue.Parse<Grouping>();
            _groupings[originalGrouping].Remove(artist);
            _groupings[updatedGrouping].RemoveWhere(a => a.Id == update.Id);
            _groupings[updatedGrouping].AddThenSort(update.UpdatedItem, a => a.Genre);
        }

        return Task.CompletedTask;
    }
}
