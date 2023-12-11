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
    
    [HttpPost("UpdateTrack")]
    public async Task UpdateTrack([FromBody] UpdatedTrackPropertiesDTO request)
    {
        await _service.UpdateTrack(request);
    }

    [HttpPost("UpdateArtist")]
    public async Task UpdateArtist([FromBody] ArtistUpdateDTO request)
    {
        await _service.UpdateArtist(request);
    }

    [HttpPost("UpdateAlbum")]
    public async Task UpdateAlbum([FromBody] AlbumUpdateDTO request)
    {
        await _service.UpdateAlbum(request);
    }

    [HttpPost("LoveTrack")]
    public async Task LoveTrack([FromBody] UpdateLovedTrackDTO request)
    {
        var username = HttpContext.GetUsername();
        await _repository.LoveTrack(username, request.TrackId);
    }

    [HttpPost("UnloveTrack")]
    public async Task UnloveTrack([FromBody] UpdateLovedTrackDTO request)
    {
        var username = HttpContext.GetUsername();
        await _repository.UnloveTrack(username, request.TrackId);
    }
}