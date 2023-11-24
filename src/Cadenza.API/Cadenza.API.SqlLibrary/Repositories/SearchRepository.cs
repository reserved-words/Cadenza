namespace Cadenza.Database.SqlLibrary.Repositories;

public class SearchRepository : ISearchRepository
{
    public async Task<List<PlayerItemDTO>> GetAlbums()
    {
        return new List<PlayerItemDTO>();
    }

    public async Task<List<PlayerItemDTO>> GetArtists()
    {
        return new List<PlayerItemDTO>();
    }

    public async Task<List<PlayerItemDTO>> GetGenres()
    {
        return new List<PlayerItemDTO>();
    }

    public async Task<List<PlayerItemDTO>> GetGroupings()
    {
        return new List<PlayerItemDTO>();
    }

    public async Task<List<PlayerItemDTO>> GetTags()
    {
        return new List<PlayerItemDTO>();
    }

    public async Task<List<PlayerItemDTO>> GetTracks()
    {
        return new List<PlayerItemDTO>();
    }
}
