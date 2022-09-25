using Cadenza.API.Common.Controllers;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;

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

    [HttpGet("GetAllTracks")]
    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _service.GetAllTracks(source);
    }

    [HttpGet("GetTracksByAlbum")]
    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId)
    {
        return await _service.GetTracksByAlbum(source, albumId);
    }

    [HttpGet("GetTracksByArtist")]
    public async Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId)
    {
        return await _service.GetTracksByArtist(source, artistId);
    }

    [HttpGet("GetUpdates")]
    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        return await _service.GetUpdates(source);
    }

    [HttpPost("MarkUpdated/{source}")]
    public async Task MarkUpdated(LibrarySource source, [FromBody] ItemUpdates update)
    {
        await _service.MarkUpdated(source, update);
    }

    [HttpPost("RemoveTrack/{source}/{id}")]
    public async Task RemoveTrack(LibrarySource source, [FromBody] string id)
    {
        await _service.RemoveTrack(source, id);
    }
}
