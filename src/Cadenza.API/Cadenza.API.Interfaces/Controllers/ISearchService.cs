namespace Cadenza.API.Interfaces.Controllers;

public interface ISearchService
{
    Task<List<PlayerItemDTO>> GetSearchAlbums();
    Task<List<PlayerItemDTO>> GetSearchArtists();
    Task<List<PlayerItemDTO>> GetSearchGenres();
    Task<List<PlayerItemDTO>> GetSearchGroupings();
    Task<List<PlayerItemDTO>> GetSearchTags();
    Task<List<PlayerItemDTO>> GetSearchTracks();
}