using Cadenza.Common.Utilities.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Cadenza.Local.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly IBase64Encoder _base64Encoder;
    private readonly ILibraryService _service;

    public LibraryController(ILibraryService service, IBase64Encoder base64Encoder)
    {
        _service = service;
        _base64Encoder = base64Encoder;
    }

    [HttpGet("Track/{idBase64}")]
    [AllowAnonymous] // Override authentication for now - later can improve this
    public async Task<IActionResult> Track(string idBase64)
    {
        var id = _base64Encoder.Decode(idBase64);

        var path = await _service.GetPlayPath(id);

        Stream stream = new FileStream(path, FileMode.Open);

        if (stream == null)
            return new NotFoundResult();

        return File(stream, "audio/mpeg");
    }
}