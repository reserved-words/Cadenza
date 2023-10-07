namespace Cadenza.API.Controllers;

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
    public async Task<List<PlayerItemDTO>> Albums()
    {
        return await _service.GetSearchAlbums();
    }

    [HttpGet("Artists")]
    public async Task<List<PlayerItemDTO>> Artists()
    {
        return await _service.GetSearchArtists();
    }

    [HttpGet("Genres")]
    public async Task<List<PlayerItemDTO>> Genres()
    {
        return await _service.GetSearchGenres();
    }

    [HttpGet("Groupings")]
    public async Task<List<PlayerItemDTO>> Groupings()
    {
        return await _service.GetSearchGroupings();
    }

    [HttpGet("Tags")]
    public async Task<List<PlayerItemDTO>> Tags()
    {
        return await _service.GetSearchTags();
    }

    [HttpGet("Tracks")]
    public async Task<List<PlayerItemDTO>> Tracks()
    {
        return await _service.GetSearchTracks();
    }
}
