using Cadenza.API.Interfaces;

namespace Cadenza.API.Core.Services;

internal class SearchService : ISearchService
{
    private readonly ILibraryCache _cache;
    private readonly ICachePopulater _populater;

    public SearchService(ILibraryCache cache, ICachePopulater populater)
    {
        _cache = cache;
        _populater = populater;
    }

    public async Task<List<PlayerItemDTO>> GetSearchAlbums()
    {
        await PopulateCache();
        return await _cache.Search.GetSearchAlbums();
    }

    public async Task<List<PlayerItemDTO>> GetSearchArtists()
    {
        await PopulateCache();
        return await _cache.Search.GetArtists();
    }

    public async Task<List<PlayerItemDTO>> GetSearchGenres()
    {
        await PopulateCache();
        return await _cache.Search.GetGenres();
    }

    public async Task<List<PlayerItemDTO>> GetSearchGroupings()
    {
        await PopulateCache();
        return await _cache.Search.GetGroupings();
    }

    public async Task<List<PlayerItemDTO>> GetSearchTags()
    {
        await PopulateCache();
        return await _cache.Search.GetTags();
    }

    public async Task<List<PlayerItemDTO>> GetSearchTracks()
    {
        await PopulateCache();
        return await _cache.Search.GetTracks();
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
