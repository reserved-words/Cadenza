using Microsoft.AspNetCore.Authorization;

namespace Cadenza.Local.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _service;

    public LibraryController(ILibraryService service)
    {
        _service = service;
    }

    [HttpGet("Track/{id}")]
    [AllowAnonymous] // Override authentication for now - later can improve this
    public async Task<IActionResult> Track(string id)
    {
        var path = await _service.GetPlayPath(id);

        Stream stream = new FileStream(path, FileMode.Open);

        if (stream == null)
            return new NotFoundResult();

        return File(stream, "audio/mpeg");
    }
}