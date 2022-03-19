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

    public async Task<ListResponse<PlayerItem>> GetSearchAlbums(int page, int limit)
    {
        return _albums.ToListResponse<SearchableAlbum, PlayerItem>(t => t.Id, page, limit);
    }

    public async Task<ListResponse<PlayerItem>> GetSearchArtists(int page, int limit)
    {
        return _artists.ToListResponse<SearchableArtist, PlayerItem>(t => t.Id, page, limit);
    }

    public async Task<ListResponse<PlayerItem>> GetSearchPlaylists(int page, int limit)
    {
        return _playlists.ToListResponse<SearchablePlaylist, PlayerItem>(t => t.Id, page, limit);
    }

    public async Task<ListResponse<PlayerItem>> GetSearchTracks(int page, int limit)
    {
        return _tracks.ToListResponse<SearchableTrack, PlayerItem>(t => t.Id, page, limit);
    }

    public async Task<ListResponse<PlayerItem>> GetSearchGenres(int page, int limit)
    {
        return _genres.ToListResponse<SearchableGenre, PlayerItem>(t => t.Id, page, limit);
    }

    public async Task<ListResponse<PlayerItem>> GetSearchGroupings(int page, int limit)
    {
        return _groupings.ToListResponse<SearchableGrouping, PlayerItem>(t => t.Id, page, limit);
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