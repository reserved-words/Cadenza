namespace Cadenza.API.Interfaces.Library;

public interface ISearchRepository
{
    Task<List<PlayerItemDTO>> GetSearchAlbums();
    Task<List<PlayerItemDTO>> GetArtists();
    Task<List<PlayerItemDTO>> GetGenres();
    Task<List<PlayerItemDTO>> GetGroupings();
    Task<List<PlayerItemDTO>> GetTags();
    Task<List<PlayerItemDTO>> GetTracks();
}