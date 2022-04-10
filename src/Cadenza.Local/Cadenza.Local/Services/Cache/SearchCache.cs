using Cadenza.Local.Common.Interfaces.Cache;

namespace Cadenza.Local.Services.Cache;

public class SearchCache : ISearchCache
{
    private List<SearchableAlbum> _albums;
    private List<SearchableArtist> _artists;
    private List<SearchablePlaylist> _playlists;
    private List<SearchableTrack> _tracks;
    private List<SearchableGenre> _genres;
    private List<SearchableGrouping> _groupings;

    public Task Populate(FullLibrary library)
    {
        _albums = library.Albums
            .Select(a => new SearchableAlbum(a.Id, a.Title, a.ArtistName))
            .ToList();

        _artists = library.Artists
            .Select(a => new SearchableArtist(a.Id, a.Name))
            .ToList();

        _groupings = library.Artists
            .Select(a => a.Grouping)
            .Distinct()
            .Select(g => new SearchableGrouping(g))
            .ToList();

        _genres = library.Artists
            .Select(a => a.Genre)
            .Distinct()
            .Select(g => new SearchableGenre(g ?? ""))
            .ToList();

        _playlists = new List<SearchablePlaylist>();

        _tracks = PopulateSearchableTracks(library);

        return Task.CompletedTask;
    }

    public Task<List<PlayerItem>> GetSearchAlbums()
    {
        var result = _albums.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchArtists()
    {
        var result = _artists.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchPlaylists()
    {
        var result = _playlists.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchTracks()
    {
        var result = _tracks.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchGenres()
    {
        var result = _genres.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchGroupings()
    {
        var result = _groupings.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    private List<SearchableTrack> PopulateSearchableTracks(FullLibrary library)
    {
        var tracks = library.Tracks;

        var artistsDict = _artists.ToDictionary(a => a.Id, a => a);
        var albumsDict = _albums.ToDictionary(a => a.Id, a => a);

        return tracks
            .Select(t =>
            {
                var artist = t.ArtistId == null
                    ? new SearchableArtist("", "No Artist Found")
                    : artistsDict[t.ArtistId];

                var album = t.AlbumId == null
                    ? new SearchableAlbum("", "No Album Found", artist.Name)
                    : albumsDict[t.AlbumId];

                return new SearchableTrack(
                    t.Id,
                    t.Title,
                    artist.Name,
                    album.Name,
                    album.Artist);
            })
            .ToList();
    }
}