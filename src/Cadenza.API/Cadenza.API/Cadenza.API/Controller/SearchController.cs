namespace Cadenza.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _service;

    public SearchController(ISearchService service)
    {
        _service = service;
    }

    [HttpGet("Albums")]
    public async Task<List<PlayerItem>> Albums()
    {
        return await _service.GetSearchAlbums();
    }

    [HttpGet("Artists")]
    public async Task<List<PlayerItem>> Artists()
    {
        return await _service.GetSearchArtists();
    }

    [HttpGet("Groupings")]
    public async Task<List<PlayerItem>> Groupings()
    {
        return await _service.GetSearchGroupings();
    }

    [HttpGet("Genres")]
    public async Task<List<PlayerItem>> Genres()
    {
        return await _service.GetSearchGenres();
    }

    [HttpGet("Tracks")]
    public async Task<List<PlayerItem>> Tracks()
    {
        return await _service.GetSearchTracks();
    }
}
