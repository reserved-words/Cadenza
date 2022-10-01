using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Repositories;

public interface ISearchRepository
{
    Task<List<PlayerItem>> GetSearchAlbums();
    Task<List<PlayerItem>> GetSearchArtists();
    Task<List<PlayerItem>> GetSearchPlaylists();
    Task<List<PlayerItem>> GetSearchTracks();
    Task<List<PlayerItem>> GetSearchGenres();
    Task<List<PlayerItem>> GetSearchGroupings();
}