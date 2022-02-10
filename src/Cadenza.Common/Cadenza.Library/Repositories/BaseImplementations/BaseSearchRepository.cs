namespace Cadenza.Library;

public class BaseSearchRepository : IBaseSearchRepository
{
    private List<SearchableAlbum> _albums;
    private List<SearchableArtist> _artists;
    private List<SearchablePlaylist> _playlists;
    private List<SearchableTrack> _tracks;

    private readonly ILibrary _library;

    public BaseSearchRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task Populate()
    {
        var library = await _library.Get();

        _albums = library.Albums
            .Select(a => new SearchableAlbum(a.Id, a.Title, a.ArtistName))
            .ToList();

        _artists = library.Artists
            .Select(a => new SearchableArtist(a.Id, a.Name))
            .ToList();

        _playlists = new List<SearchablePlaylist>();

        _tracks = PopulateSearchableTracks(library);
    }

    public async Task<ListResponse<SearchableItem>> GetSearchAlbums(int page, int limit)
    {
        return _albums.ToListResponse<SearchableAlbum, SearchableItem>(t => t.Id, page, limit);
    }

    public async Task<ListResponse<SearchableItem>> GetSearchArtists(int page, int limit)
    {
        return _artists.ToListResponse<SearchableArtist, SearchableItem>(t => t.Id, page, limit);
    }

    public async Task<ListResponse<SearchableItem>> GetSearchPlaylists(int page, int limit)
    {
        return _playlists.ToListResponse<SearchablePlaylist, SearchableItem>(t => t.Id, page, limit);
    }

    public async Task<ListResponse<SearchableItem>> GetSearchTracks(int page, int limit)
    {
        return _tracks.ToListResponse<SearchableTrack, SearchableItem>(t => t.Id, page, limit);
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