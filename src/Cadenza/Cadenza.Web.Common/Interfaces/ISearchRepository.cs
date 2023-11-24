namespace Cadenza.Web.Common.Interfaces;

public interface ISearchRepository
{
    Task<List<SearchItemVM>> GetSearchAlbums();
    Task<List<SearchItemVM>> GetArtists();
    Task<List<SearchItemVM>> GetGenres();
    Task<List<SearchItemVM>> GetGroupings();
    Task<List<SearchItemVM>> GetTags();
    Task<List<SearchItemVM>> GetTracks();
}