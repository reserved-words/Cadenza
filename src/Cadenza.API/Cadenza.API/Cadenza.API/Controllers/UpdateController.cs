﻿namespace Cadenza.API.Controllers;

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
    public async Task RemoveTrack([FromBody] TrackRemovalRequest request)
    {
        await _service.RemoveTrack(request);
    }

    [HttpPost("UpdateTrack/{source}")]
    public async Task UpdateTrack(LibrarySource source, [FromBody] ItemUpdateRequest request)
    {
        await _service.UpdateTrack(source, request);
    }

    [HttpPost("UpdateArtist")]
    public async Task UpdateArtist([FromBody] ItemUpdateRequest request)
    {
        await _service.UpdateArtist(request);
    }

    [HttpPost("UpdateAlbum/{source}")]
    public async Task UpdateAlbum(LibrarySource source, [FromBody] ItemUpdateRequest request)
    {
        await _service.UpdateAlbum(source, request);
    }
}