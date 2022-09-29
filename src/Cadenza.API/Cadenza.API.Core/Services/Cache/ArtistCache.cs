using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Extensions;
using Cadenza.Common.Domain.Model;
using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;
using Cadenza.Common.Domain.Model.Update;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.API.Core.Services.Cache;

internal class ArtistCache : IArtistCache
{
    private Dictionary<string, ArtistInfo> _artists;
    private Dictionary<string, List<AlbumInfo>> _albums;
    private List<string> _albumArtists;
    private List<string> _trackArtists;
    private Dictionary<Grouping, List<ArtistInfo>> _groupings;
    private Dictionary<string, List<ArtistInfo>> _genres;

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

    public Task Populate(FullLibrary library)
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
                var ex = new Exception($"Artist ID is null for {album.Id} ({album.Title})");
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
