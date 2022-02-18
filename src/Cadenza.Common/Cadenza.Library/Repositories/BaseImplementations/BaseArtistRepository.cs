using Newtonsoft.Json;

namespace Cadenza.Library;

public class BaseArtistRepository : IBaseArtistRepository
{
    private readonly ILibrary _library;

    private Dictionary<string, ArtistInfo> _artists;
    private Dictionary<string, List<AlbumInfo>> _albums;
    private List<string> _albumArtists;
    private List<string> _trackArtists;

    public BaseArtistRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit)
    {
        return _albumArtists
            .Select(a => _artists[a])
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);
    }

    public async Task<ListResponse<Album>> GetAlbums(string artistId, int page, int limit)
    {
        return _albums[artistId]
            .ToListResponse<Album>(a => a.Id, page, limit);
    }

    public async Task<ListResponse<Artist>> GetAllArtists(int page, int limit)
    {
        return _artists
            .Values
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);
    }

    public async Task<ArtistInfo> GetArtist(string id)
    {
        return _artists[id];
    }

    public async Task<ListResponse<Artist>> GetTrackArtists(int page, int limit)
    {
        return _trackArtists
            .Select(a => _artists[a])
            .ToListResponse<ArtistInfo, Artist>(a => a.Id, page, limit);
    }

    public async Task Populate()
    {
        var library = await _library.Get();

        _artists = library.Artists.ToDictionary(a => a.Id, a => a);
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
}
