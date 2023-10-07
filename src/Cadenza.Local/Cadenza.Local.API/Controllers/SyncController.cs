namespace Cadenza.Local.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SyncController : ControllerBase
{
    private readonly IBase64Encoder _base64Encoder;
    private readonly ISyncService _service;

    public SyncController(ISyncService service, IBase64Encoder base64Encoder)
    {
        _service = service;
        _base64Encoder = base64Encoder;
    }

    [HttpGet("GetAllTracks")]
    public async Task<List<string>> GetAllTracks()
    {
        return await _service.GetAllTracks();
    }

    [HttpGet("GetTrack/{idBase64}")]
    public async Task<SyncTrackDTO> GetTrack(string idBase64)
    {
        var id = _base64Encoder.Decode(idBase64);
        return await _service.GetTrack(id);
    }

    [HttpPost("UpdateTracks")]
    public async Task UpdateTracks([FromBody] MultiTrackUpdatesDTO updates)
    {
        await _service.UpdateTracks(updates);
    }

    [HttpDelete("RemoveTrack")]
    public async Task RemoveTracks([FromBody] SyncTrackRemovalRequestDTO request)
    {
        await _service.RemoveTrack(request.TrackIdFromSource);
    }
}