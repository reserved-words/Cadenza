namespace Cadenza.Library;

public interface IArtistRepository
{
    Task Populate();
    Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit);
    Task<ListResponse<Artist>> GetAllArtists(int page, int limit);
    Task<ListResponse<Artist>> GetTrackArtists(int page, int limit);
    Task<ArtistInfo> GetArtist(string id);
    Task<ListResponse<AlbumInfo>> GetAlbums(string artistId, int page, int limit);
}
