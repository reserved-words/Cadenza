namespace Cadenza.Library;

public class SearchCache : ISearchCache
{
    private List<SearchableAlbum> _albums;
    private List<SearchableArtist> _artists;
    private List<SearchablePlaylist> _playlists;
    private List<SearchableTrack> _tracks;
    private List<SearchableGenre> _genres;
    private List<SearchableGrouping> _groupings;

    public async Task Populate(FullLibrary library)
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
    }

    public async Task<List<PlayerItem>> GetSearchAlbums()
    {
        return _albums.OfType<PlayerItem>().ToList();
    }

    public async Task<List<PlayerItem>> GetSearchArtists()
    {
        return _artists.OfType<PlayerItem>().ToList();
    }

    public async Task<List<PlayerItem>> GetSearchPlaylists()
    {
        return _playlists.OfType<PlayerItem>().ToList();
    }

    public async Task<List<PlayerItem>> GetSearchTracks()
    {
        return _tracks.OfType<PlayerItem>().ToList();
    }

    public async Task<List<PlayerItem>> GetSearchGenres()
    {
        return _genres.OfType<PlayerItem>().ToList();
    }

    public async Task<List<PlayerItem>> GetSearchGroupings()
    {
        return _groupings.OfType<PlayerItem>().ToList();
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