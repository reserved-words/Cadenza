using Cadenza.API.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;

[Route("[controller]")]
[ApiController]
public class PlayController : ControllerBase
{
    private readonly IPlayTrackService _service;

    public PlayController(IPlayTrackService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<PlayTrack>> Tracks()
    {
        return await _service.GetPlayTracks();
    }

    [HttpGet]
    public async Task<List<PlayTrack>> Artist(string id)
    {
        return await _service.GetPlayTracksByArtist(id);
    }

    [HttpGet]
    public async Task<List<PlayTrack>> Album(string id)
    {
        return await _service.GetPlayTracksByAlbum(id);
    }

    [HttpGet]
    public async Task<List<PlayTrack>> Genre(string id)
    {
        return await _service.GetPlayTracksByGenre(id);
    }

    [HttpGet]
    public async Task<List<PlayTrack>> Grouping(Grouping id)
    {
        return await _service.GetPlayTracksByGrouping(id);
    }
}
