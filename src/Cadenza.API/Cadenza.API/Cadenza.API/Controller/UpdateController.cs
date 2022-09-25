using Cadenza.API.Common.Controllers;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UpdateController : ControllerBase
{
    private readonly IUpdateService _service;

    public UpdateController(IUpdateService service)
    {
        _service = service;
    }

    //[HttpGet("Get")]
    //public async Task<List<ItemUpdates>> Get()
    //{
    //    return await _service.GetQueuedUpdates();
    //}

    //[HttpPost("Remove")]
    //public async Task Update(ItemUpdates update)
    //{
    //    await _service.Update(update);
    //}

    [HttpPost("UpdateTrack/{source}")]
    public async Task UpdateTrack(LibrarySource source, [FromBody]ItemUpdates update)
    {
        await _service.UpdateTrack(source, update);
    }

    [HttpPost("UpdateArtist/{source}")]
    public async Task UpdateArtist([FromBody] ItemUpdates update)
    {
        await _service.UpdateArtist(update);
    }

    [HttpPost("UpdateAlbum/{source}")]
    public async Task UpdateAlbum(LibrarySource source, [FromBody] ItemUpdates update)
    {
        await _service.UpdateAlbum(source, update);
    }
}