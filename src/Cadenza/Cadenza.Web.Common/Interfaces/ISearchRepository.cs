namespace Cadenza.Web.Common.Interfaces;

public interface ISearchRepository
{
    Task<List<PlayerItemVM>> GetSearchAlbums();
    Task<List<PlayerItemVM>> GetArtists();
    Task<List<PlayerItemVM>> GetGenres();
    Task<List<PlayerItemVM>> GetGroupings();
    Task<List<PlayerItemVM>> GetTags();
    Task<List<PlayerItemVM>> GetTracks();
}