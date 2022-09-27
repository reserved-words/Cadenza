using Cadenza.Domain.Model;

namespace Cadenza.API.Interfaces.Controllers;

public interface ISearchService
{
    Task<List<PlayerItem>> GetSearchAlbums();
    Task<List<PlayerItem>> GetSearchArtists();
    Task<List<PlayerItem>> GetSearchGenres();
    Task<List<PlayerItem>> GetSearchGroupings();
    Task<List<PlayerItem>> GetSearchTracks();
}