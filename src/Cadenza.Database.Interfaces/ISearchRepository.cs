namespace Cadenza.Database.Interfaces;

public interface ISearchRepository
{
    Task<List<PlayerItemDTO>> GetAlbums();
    Task<List<PlayerItemDTO>> GetArtists();
    Task<List<PlayerItemDTO>> GetGenres();
    Task<List<PlayerItemDTO>> GetGroupings();
    Task<List<PlayerItemDTO>> GetTags();
    Task<List<PlayerItemDTO>> GetTracks();
}
