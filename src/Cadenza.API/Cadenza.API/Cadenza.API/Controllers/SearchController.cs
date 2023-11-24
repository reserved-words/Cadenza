using Cadenza.API.Interfaces.Services;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ILibraryCache _cache;
    private readonly ICachePopulater _populater;

    public SearchController(ILibraryCache cache, ICachePopulater populater)
    {
        _cache = cache;
        _populater = populater;
    }

    [HttpGet("Albums")]
    public async Task<List<PlayerItemDTO>> Albums()
    {
        await PopulateCache();
        return await _cache.Search.GetSearchAlbums();
    }

    [HttpGet("Artists")]
    public async Task<List<PlayerItemDTO>> Artists()
    {
        await PopulateCache();
        return await _cache.Search.GetArtists();
    }

    [HttpGet("Genres")]
    public async Task<List<PlayerItemDTO>> Genres()
    {
        await PopulateCache();
        return await _cache.Search.GetGenres();
    }

    [HttpGet("Groupings")]
    public async Task<List<PlayerItemDTO>> Groupings()
    {
        await PopulateCache();
        return await _cache.Search.GetGroupings();
    }

    [HttpGet("Tags")]
    public async Task<List<PlayerItemDTO>> Tags()
    {
        await PopulateCache();
        return await _cache.Search.GetTags();
    }

    [HttpGet("Tracks")]
    public async Task<List<PlayerItemDTO>> Tracks()
    {
        await PopulateCache();
        return await _cache.Search.GetTracks();
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
