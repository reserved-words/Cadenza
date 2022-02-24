namespace Cadenza.Library;

public interface ISearchRepository
{
    Task<ListResponse<PlayerItem>> GetSearchAlbums(int page, int limit);
    Task<ListResponse<PlayerItem>> GetSearchArtists(int page, int limit);
    Task<ListResponse<PlayerItem>> GetSearchPlaylists(int page, int limit);
    Task<ListResponse<PlayerItem>> GetSearchTracks(int page, int limit);
}