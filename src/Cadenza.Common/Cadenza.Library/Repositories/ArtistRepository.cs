namespace Cadenza.Library;

public class ArtistRepository : IArtistRepository
{
    private readonly ILibrary _library;

    private Dictionary<string, ArtistInfo> _artists;
    private Dictionary<string, List<AlbumInfo>> _albums;
    private List<string> _albumArtists;
    private List<string> _trackArtists;

    public ArtistRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit)
    {
        if (_artists == null)
        {
            await Populate();
        }

        return _albumArtists
            .Select(a => _artists[a])
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);
    }

    public async Task<ListResponse<AlbumInfo>> GetAlbums(string artistId, int page, int limit)
    {
        if (_albums == null)
        {
            await Populate();
        }

        return _albums[artistId]
            .ToListResponse<AlbumInfo>(a => a.Id, page, limit);
    }

    public async Task<ListResponse<Artist>> GetAllArtists(int page, int limit)
    {
        if (_artists == null)
        {
            await Populate();
        }

        return _artists
            .Values
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);
    }

    public async Task<ArtistInfo> GetArtist(string id)
    {
        if (_artists == null)
        {
            await Populate();
        }

        return _artists[id];
    }

    public async Task<ListResponse<Artist>> GetTrackArtists(int page, int limit)
    {
        if (_artists == null)
        {
            await Populate();
        }

        return _trackArtists
            .Select(a => _artists[a])
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);
    }

    public async Task Populate()
    {
        if (!_library.IsPopulated)
        {
            await _library.Populate();
        }

        var library = await _library.Get();

        _artists = library.Artists.ToDictionary(a => a.Id, a => a);
        _albums = library.Artists.ToDictionary(a => a.Id, a => new List<AlbumInfo>());
        _albumArtists = library.Albums.Select(a => a.ArtistId).Distinct().ToList();
        _trackArtists = library.Tracks.Select(a => a.ArtistId).Distinct().ToList();

        foreach (var album in library.Albums)
        {
            _albums[album.ArtistId].Add(album);
        }
    }
}
