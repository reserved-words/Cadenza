using Cadenza.API.Common.Controllers;
using Cadenza.Domain.Models;

namespace Cadenza.API.Core;

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
        return await _cache.SearchCache.GetSearchTracks();
    }

    public async Task<List<PlayerItem>> GetSearchGroupings()
    {
        await PopulateCache();
        return await _cache.SearchCache.GetSearchGroupings();
    }

    public async Task<List<PlayerItem>> GetSearchGenres()
    {
        await PopulateCache();
        return await _cache.SearchCache.GetSearchGenres();
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
