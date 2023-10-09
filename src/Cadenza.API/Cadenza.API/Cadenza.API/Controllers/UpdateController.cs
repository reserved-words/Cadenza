namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UpdateController : ControllerBase
{
    private readonly IUpdateService _service;

    public UpdateController(IUpdateService service)
    {
        _service = service;
    }

    [HttpDelete("RemoveTrack")]
    public async Task RemoveTrack([FromBody] TrackRemovalRequestDTO request)
    {
        await _service.RemoveTrack(request);
    }

    [HttpPost("UpdateTrack")]
    public async Task UpdateTrack([FromBody] UpdateTrackDTO request)
    {
        await _service.UpdateTrack(request);
    }

    [HttpPost("UpdateArtist")]
    public async Task UpdateArtist([FromBody] UpdateArtistDTO request)
    {
        await _service.UpdateArtist(request);
    }

    [HttpPost("UpdateAlbum")]
    public async Task UpdateAlbum([FromBody] UpdateAlbumDTO request)
    {
        await _service.UpdateAlbum(request);
    }
}