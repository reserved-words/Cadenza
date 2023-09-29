namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayController : ControllerBase
{
    private readonly IPlayTrackService _service;

    public PlayController(IPlayTrackService service)
    {
        _service = service;
    }

    [HttpGet("Album/{id}")]
    public async Task<List<int>> Album(int id)
    {
        return await _service.GetPlayTracksByAlbum(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<List<int>> Artist(int id)
    {
        return await _service.GetPlayTracksByArtist(id);
    }

    [HttpGet("Genre/{id}")]
    public async Task<List<int>> Genre(string id)
    {
        return await _service.GetPlayTracksByGenre(id);
    }

    [HttpGet("Grouping/{id}")]
    public async Task<List<int>> Grouping(int id)
    {
        return await _service.GetPlayTracksByGrouping(id);
    }

    [HttpGet("Tag/{id}")]
    public async Task<List<int>> Tag(string id)
    {
        return await _service.GetPlayTracksByTag(id);
    }

    [HttpGet("Tracks")]
    public async Task<List<int>> Tracks()
    {
        return await _service.GetPlayTracks();
    }
}
