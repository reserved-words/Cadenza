using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Track;
using Cadenza.Common.Domain.Model.Updates;

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
    public async Task AddTrack(LibrarySource source, [FromBody] TrackFull track)
    {
        await _service.AddTrack(source, track);
    }

    [HttpGet("GetAllTracks/{source}")]
    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _service.GetAllTracks(source);
    }

    [HttpGet("GetTracksByAlbum/{source}/{albumId}")]
    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId)
    {
        return await _service.GetTracksByAlbum(source, albumId);
    }

    [HttpGet("GetTracksByArtist/{source}/{artistId}")]
    public async Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId)
    {
        return await _service.GetTracksByArtist(source, artistId);
    }

    [HttpGet("GetUpdates/{source}")]
    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        return await _service.GetUpdates(source);
    }

    [HttpPost("MarkUpdated/{source}")]
    public async Task MarkUpdated(LibrarySource source, [FromBody] ItemUpdates update)
    {
        await _service.MarkUpdated(source, update);
    }

    [HttpPost("RemoveTracks/{source}")]
    public async Task RemoveTrack(LibrarySource source, [FromBody] List<string> ids)
    {
        await _service.RemoveTracks(source, ids);
    }
}
