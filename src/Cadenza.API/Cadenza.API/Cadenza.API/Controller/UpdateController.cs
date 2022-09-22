using Cadenza.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;
[Route("api/[controller]")]
[ApiController]
public class UpdateController : ControllerBase
{
    private readonly IUpdateService _service;

    public UpdateController(IUpdateService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<object> Get()
    {
        return await _service.GetUpdates();
    }

    [HttpPost]
    public async Task Album(AlbumUpdate update)
    {
        await _service.UpdateAlbum(update);
    }

    [HttpPost]
    public async Task Artist(ArtistUpdate update)
    {
        await _service.UpdateArtist(update);
    }

    [HttpPost]
    public async Task Track(TrackUpdate update)
    {
        await _service.UpdateTrack(update);
    }
}
