using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UpdateController : ControllerBase
{
    private readonly IUpdateService _service;

    public UpdateController(IUpdateService service)
    {
        _service = service;
    }

    [HttpPost("UpdateTrack/{source}")]
    public async Task UpdateTrack(LibrarySource source, [FromBody] ItemUpdates update)
    {
        await _service.UpdateTrack(source, update);
    }

    [HttpPost("UpdateArtist")]
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