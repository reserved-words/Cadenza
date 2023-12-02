namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayController : ControllerBase
{
    private readonly IPlayRepository _repository;
    private readonly IShuffler _shuffler;

    public PlayController(IPlayRepository repository, IShuffler shuffler)
    {
        _repository = repository;
        _shuffler = shuffler;
    }

    [HttpGet("Album/{id}")]
    public async Task<List<int>> Album(int id)
    {
        return await _repository.PlayAlbum(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<List<int>> Artist(int id)
    {
        var tracks = await _repository.PlayArtist(id);
        _shuffler.Shuffle(tracks);
        return tracks;
    }

    [HttpGet("Genre/{id}")]
    public async Task<List<int>> Genre(string id)
    {
        var tracks = await _repository.PlayGenre(id);
        _shuffler.Shuffle(tracks);
        return tracks;
    }

    [HttpGet("Grouping/{id}")]
    public async Task<List<int>> Grouping(int id)
    {
        var tracks = await _repository.PlayGrouping(id);
        _shuffler.Shuffle(tracks);
        return tracks;
    }

    [HttpGet("Tag/{id}")]
    public async Task<List<int>> Tag(string id)
    {
        var tracks = await _repository.PlayTag(id);
        _shuffler.Shuffle(tracks);
        return tracks;
    }

    [HttpGet("Tracks")]
    public async Task<List<int>> Tracks()
    {
        var tracks = await _repository.PlayAll();
        _shuffler.Shuffle(tracks);
        return tracks;
    }
}
