namespace Cadenza.Web.Api.Interfaces;

public interface ISearchApi
{
    Task<List<SearchItemVM>> GetAlbums();
    Task<List<SearchItemVM>> GetArtists();
    Task<List<SearchItemVM>> GetGenres();
    Task<List<SearchItemVM>> GetGroupings();
    Task<List<SearchItemVM>> GetTags();
    Task<List<SearchItemVM>> GetTracks();
}