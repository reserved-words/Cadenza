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

    [HttpGet("Album")]
    public async Task<List<PlayTrack>> Album(int id)
    {
        return await _service.GetPlayTracksByAlbum(id);
    }

    [HttpGet("Artist")]
    public async Task<List<PlayTrack>> Artist(int id)
    {
        return await _service.GetPlayTracksByArtist(id);
    }

    [HttpGet("Genre")]
    public async Task<List<PlayTrack>> Genre(string id)
    {
        return await _service.GetPlayTracksByGenre(id);
    }

    [HttpGet("Grouping")]
    public async Task<List<PlayTrack>> Grouping(Grouping id)
    {
        return await _service.GetPlayTracksByGrouping(id);
    }

    [HttpGet("Tag")]
    public async Task<List<PlayTrack>> Tag(string id)
    {
        return await _service.GetPlayTracksByTag(id);
    }

    [HttpGet("Tracks")]
    public async Task<List<PlayTrack>> Tracks()
    {
        return await _service.GetPlayTracks();
    }
}
