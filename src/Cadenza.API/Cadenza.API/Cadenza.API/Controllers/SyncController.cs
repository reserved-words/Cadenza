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

    [HttpGet("GetTracksByAlbum/{source}/{albumId}")]
    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, int albumId)
    {
        return await _service.GetAlbumTrackSourceIds(source, albumId);
    }

    [HttpGet("GetTracksByArtist/{source}/{artistId}")]
    public async Task<List<string>> GetTracksByArtist(LibrarySource source, int artistId)
    {
        return await _service.GetArtistTrackSourceIds(source, artistId);
    }

    [HttpGet("GetUpdateRequests/{source}")]
    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
    {
        return await _service.GetUpdateRequests(source);
    }

    [HttpPost("MarkUpdateErrored/{source}")]
    public async Task MarkUpdateErrored(LibrarySource source, [FromBody] ItemUpdateRequest request)
    {
        await _service.MarkUpdateErrored(source, request);
    }

    [HttpPost("MarkUpdateDone/{source}")]
    public async Task MarkUpdateDone(LibrarySource source, [FromBody] ItemUpdateRequest request)
    {
        await _service.MarkUpdateDone(source, request);
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
