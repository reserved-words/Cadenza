using Cadenza.API.Interfaces.Services;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UpdateController : ControllerBase
{
    private readonly IUpdateRepository _repository;
    private readonly IUpdateService _service;

    public UpdateController(IUpdateService service, IUpdateRepository repository)
    {
        _service = service;
        _repository = repository;
    }

    [HttpDelete("UpdateAlbumTracks")]
    public async Task UpdateAlbumTracks([FromBody] UpdateAlbumTracksDTO request)
    {
        await _service.UpdateAlbumTracks(request);
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

    [HttpPost("LoveTrack")]
    public async Task LoveTrack([FromBody] UpdateLovedTrackDTO request)
    {
        await _repository.LoveTrack(request.TrackId);
    }

    [HttpPost("UnloveTrack")]
    public async Task UnloveTrack([FromBody] UpdateLovedTrackDTO request)
    {
        await _repository.UnloveTrack(request.TrackId);
    }
}