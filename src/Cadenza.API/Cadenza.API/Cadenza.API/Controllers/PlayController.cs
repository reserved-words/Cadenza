using Cadenza.API.Interfaces.Services;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayController : ControllerBase
{
    private readonly ILibraryCache _cache;
    private readonly ICachePopulater _populater;

    public PlayController(ILibraryCache cache, ICachePopulater populater)
    {
        _cache = cache;
        _populater = populater;
    }

    [HttpGet("Album/{id}")]
    public async Task<List<int>> Album(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayAlbum(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<List<int>> Artist(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayArtist(id);
    }

    [HttpGet("Genre/{id}")]
    public async Task<List<int>> Genre(string id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayGenre(id);
    }

    [HttpGet("Grouping/{id}")]
    public async Task<List<int>> Grouping(int id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayGrouping(id);
    }

    [HttpGet("Tag/{id}")]
    public async Task<List<int>> Tag(string id)
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayTag(id);
    }

    [HttpGet("Tracks")]
    public async Task<List<int>> Tracks()
    {
        await PopulateCache();
        return await _cache.PlayTracks.PlayAll();
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
