namespace Cadenza.Local.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SyncController : ControllerBase
{
    private readonly ISyncService _service;

    public SyncController(ISyncService service)
    {
        _service = service;
    }

    [HttpGet("GetAllTracks")]
    public async Task<List<string>> GetAllTracks()
    {
        return await _service.GetAllTracks();
    }

    [HttpGet("GetTrack/{id}")]
    public async Task<TrackFull> GetTrack(string id)
    {
        return await _service.GetTrack(id);
    }

    [HttpPost("UpdateTracks")]
    public async Task UpdateTracks([FromBody] MultiTrackUpdates updates)
    {
        await _service.UpdateTracks(updates);
    }
}