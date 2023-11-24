namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchRepository _repository;

    public SearchController(ISearchRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("Albums")]
    public async Task<List<PlayerItemDTO>> Albums()
    {
        return await _repository.GetAlbums();
    }

    [HttpGet("Artists")]
    public async Task<List<PlayerItemDTO>> Artists()
    {
        return await _repository.GetArtists();
    }

    [HttpGet("Genres")]
    public async Task<List<PlayerItemDTO>> Genres()
    {
        return await _repository.GetGenres();
    }

    [HttpGet("Groupings")]
    public async Task<List<PlayerItemDTO>> Groupings()
    {
        return await _repository.GetGroupings();
    }

    [HttpGet("Tags")]
    public async Task<List<PlayerItemDTO>> Tags()
    {
        return await _repository.GetTags();
    }

    [HttpGet("Tracks")]
    public async Task<List<PlayerItemDTO>> Tracks()
    {
        return await _repository.GetTracks();
    }
}
