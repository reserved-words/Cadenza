using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Repositories;

public interface ISearchRepository
{
    Task<List<PlayerItem>> GetSearchAlbums();
    Task<List<PlayerItem>> GetArtists();
    Task<List<PlayerItem>> GetGenres();
    Task<List<PlayerItem>> GetGroupings();
    Task<List<PlayerItem>> GetTags();
    Task<List<PlayerItem>> GetTracks();
}