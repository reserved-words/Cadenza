namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayController : ControllerBase
{
    private readonly IPlayRepository _repository;

    public PlayController(IPlayRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("Album/{id}")]
    public async Task<List<int>> Album(int id)
    {
        return await _repository.PlayAlbum(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<List<int>> Artist(int id)
    {
        return await _repository.PlayArtist(id);
    }

    [HttpGet("Genre/{id}")]
    public async Task<List<int>> Genre(string id)
    {
        return await _repository.PlayGenre(id);
    }

    [HttpGet("Grouping/{id}")]
    public async Task<List<int>> Grouping(int id)
    {
        return await _repository.PlayGrouping(id);
    }

    [HttpGet("Tag/{id}")]
    public async Task<List<int>> Tag(string id)
    {
        return await _repository.PlayTag(id);
    }

    [HttpGet("Tracks")]
    public async Task<List<int>> Tracks()
    {
        return await _repository.PlayAll();
    }
}
