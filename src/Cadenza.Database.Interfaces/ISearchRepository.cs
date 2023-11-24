namespace Cadenza.Database.Interfaces;

public interface ISearchRepository
{
    Task<List<SearchItemDTO>> GetAlbums();
    Task<List<SearchItemDTO>> GetArtists();
    Task<List<SearchItemDTO>> GetGenres();
    Task<List<SearchItemDTO>> GetGroupings();
    Task<List<SearchItemDTO>> GetTags();
    Task<List<SearchItemDTO>> GetTracks();
}
