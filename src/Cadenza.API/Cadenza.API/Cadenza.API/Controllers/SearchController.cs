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
    public async Task<List<SearchItemDTO>> Albums()
    {
        return await _repository.GetAlbums();
    }

    [HttpGet("Artists")]
    public async Task<List<SearchItemDTO>> Artists()
    {
        return await _repository.GetArtists();
    }

    [HttpGet("Genres")]
    public async Task<List<SearchItemDTO>> Genres()
    {
        return await _repository.GetGenres();
    }

    [HttpGet("Groupings")]
    public async Task<List<SearchItemDTO>> Groupings()
    {
        return await _repository.GetGroupings();
    }

    [HttpGet("Tags")]
    public async Task<List<SearchItemDTO>> Tags()
    {
        return await _repository.GetTags();
    }

    [HttpGet("Tracks")]
    public async Task<List<SearchItemDTO>> Tracks()
    {
        return await _repository.GetTracks();
    }
}
