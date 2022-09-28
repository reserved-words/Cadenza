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

    [HttpGet("Artwork/{id}")]
    public async Task Artwork(string id)
    {
        var artwork = await _service.GetArtwork(id);
        Response.ContentType = artwork.Type;
        Response.ContentLength = artwork.Bytes.Length;
        var readOnlyMemory = new ReadOnlyMemory<byte>(artwork.Bytes);
        await Response.BodyWriter.WriteAsync(readOnlyMemory);
    }

    [HttpGet("Track/{id}")]
    public async Task<IActionResult> Track(string id)
    {
        var path = await _service.GetPlayPath(id);

        Stream stream = new FileStream(path, FileMode.Open);

        if (stream == null)
            return new NotFoundResult();

        return File(stream, "audio/mpeg");
    }
}