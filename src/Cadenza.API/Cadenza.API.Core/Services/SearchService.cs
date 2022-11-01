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

    public async Task<List<PlayerItem>> GetSearchAlbums()
    {
        await PopulateCache();
        return await _cache.SearchCache.GetSearchAlbums();
    }

    public async Task<List<PlayerItem>> GetSearchArtists()
    {
        await PopulateCache();
        return await _cache.SearchCache.GetSearchArtists();
    }

    public async Task<List<PlayerItem>> GetSearchGenres()
    {
        await PopulateCache();
        return await _cache.SearchCache.GetSearchGenres();
    }

    public async Task<List<PlayerItem>> GetSearchGroupings()
    {
        await PopulateCache();
        return await _cache.SearchCache.GetSearchGroupings();
    }

    public async Task<List<PlayerItem>> GetSearchTags()
    {
        await PopulateCache();
        return await _cache.SearchCache.GetSearchTags();
    }

    public async Task<List<PlayerItem>> GetSearchTracks()
    {
        await PopulateCache();
        return await _cache.SearchCache.GetSearchTracks();
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
