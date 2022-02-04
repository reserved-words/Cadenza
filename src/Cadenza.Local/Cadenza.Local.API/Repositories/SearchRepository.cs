using Cadenza.Library;

namespace Cadenza.Local.API;

public interface ILocalSearchRepository : ISearchRepository
{
    Task Populate();
}

public class SearchRepository : ILocalSearchRepository
{
    private List<SearchableAlbum> _albums;
    private List<SearchableArtist> _artists;
    private List<SearchablePlaylist> _playlists;
    private List<SearchableTrack> _tracks;

    private readonly ILibrary _library;

    public SearchRepository(ILibrary library)
    {
        _library = library;
    }

    public LibrarySource Source => LibrarySource.Local;

    public async Task Populate()
    {
        _albums = (await _library.GetAlbums())
            .Select(a => new SearchableAlbum(a.Id, a.Title, a.ArtistName))
            .ToList();

        _artists = (await _library.GetArtists())
            .Select(a => new SearchableArtist(a.Id, a.Name))
            .ToList();

        _playlists = new List<SearchablePlaylist>();

        _tracks = await PopulateSearchableTracks();
    }

    public async Task<ListResponse<SearchableItem>> GetSearchAlbums(int page, int limit)
    {
        return await GetListResponse(_albums, page, limit);
    }

    public async Task<ListResponse<SearchableItem>> GetSearchArtists(int page, int limit)
    {
        return await GetListResponse(_artists, page, limit);
    }

    public async Task<ListResponse<SearchableItem>> GetSearchPlaylists(int page, int limit)
    {
        return await GetListResponse(_playlists, page, limit);
    }

    public async Task<ListResponse<SearchableItem>> GetSearchTracks(int page, int limit)
    {
        return await GetListResponse(_tracks, page, limit);
    }

    private async Task<ListResponse<SearchableItem>> GetListResponse<T>(List<T> allItems, int page, int limit) where T : SearchableItem
    {
        if (allItems == null)
        {
            await Populate();
        }

        var skip = (page - 1) * limit;

        var items = allItems
            .OrderBy(i => i.Id)
            .Skip(skip)
            .Take(limit)
            .OfType<SearchableItem>()
            .ToList();

        var total = allItems.Count;

        return new ListResponse<SearchableItem>
        {
            Items = items,
            Limit = limit,
            Page = page,
            TotalItems = total,
            TotalPages = (int)Math.Ceiling((double)total / limit)
        };
    }

    private async Task<List<SearchableTrack>> PopulateSearchableTracks()
    {
        var tracks = await _library.GetAllTracks();

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