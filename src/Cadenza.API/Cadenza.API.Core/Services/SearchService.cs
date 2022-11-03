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
        return await _cache.Search.GetSearchAlbums();
    }

    public async Task<List<PlayerItem>> GetSearchArtists()
    {
        await PopulateCache();
        return await _cache.Search.GetArtists();
    }

    public async Task<List<PlayerItem>> GetSearchGenres()
    {
        await PopulateCache();
        return await _cache.Search.GetGenres();
    }

    public async Task<List<PlayerItem>> GetSearchGroupings()
    {
        await PopulateCache();
        return await _cache.Search.GetGroupings();
    }

    public async Task<List<PlayerItem>> GetSearchTags()
    {
        await PopulateCache();
        return await _cache.Search.GetTags();
    }

    public async Task<List<PlayerItem>> GetSearchTracks()
    {
        await PopulateCache();
        return await _cache.Search.GetTracks();
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
