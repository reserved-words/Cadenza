namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SyncController : ControllerBase
{
    private readonly ISyncService _service;

    public SyncController(ISyncService service)
    {
        _service = service;
    }

    [HttpPost("AddTrack/{source}")]
    public async Task AddTrack(LibrarySource source, [FromBody] SyncTrack track)
    {
        await _service.AddTrack(source, track);
    }

    [HttpGet("GetAllTracks/{source}")]
    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _service.GetAllTrackSourceIds(source);
    }

    [HttpGet("GetTrackIdFromSource/{trackId}")]
    public async Task<SyncSourceTrack> GetTrackIdFromSource(int trackId)
    {
        return await _service.GetTrackIdFromSource(trackId);
    }

    [HttpGet("GetTracksByAlbum/{albumId}")]
    public async Task<List<string>> GetTracksByAlbum(int albumId)
    {
        return await _service.GetAlbumTrackSourceIds(albumId);
    }

    [HttpGet("GetTracksByArtist/{artistId}")]
    public async Task<List<string>> GetTracksByArtist(int artistId)
    {
        return await _service.GetArtistTrackSourceIds(artistId);
    }

    [HttpGet("GetUpdateRequests/{source}")]
    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
    {
        return await _service.GetUpdateRequests(source);
    }

    [HttpPost("MarkUpdateErrored")]
    public async Task MarkUpdateErrored([FromBody] ItemUpdateRequest request)
    {
        await _service.MarkUpdateErrored(request);
    }

    [HttpPost("MarkUpdateDone")]
    public async Task MarkUpdateDone([FromBody] ItemUpdateRequest request)
    {
        await _service.MarkUpdateDone(request);
    }

    [HttpGet("GetRemovalRequests/{source}")]
    public async Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source)
    {
        return await _service.GetRemovalRequests(source);
    }

    [HttpPost("MarkRemovalErrored")]
    public async Task MarkRemovalErrored([FromBody] SyncTrackRemovalRequest request)
    {
        await _service.MarkRemovalErrored(request);
    }

    [HttpPost("MarkRemovalDone")]
    public async Task MarkRemovalDone([FromBody] SyncTrackRemovalRequest request)
    {
        await _service.MarkRemovalDone(request);
    }

    [HttpPost("RemoveTracks/{source}")]
    public async Task RemoveTracks(LibrarySource source, [FromBody] List<string> idsFromSource)
    {
        await _service.RemoveTracks(source, idsFromSource);
    }
}
