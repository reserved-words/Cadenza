using Microsoft.AspNetCore.Authorization;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous] // Override authentication for now - later can improve this
public class ImageController : ControllerBase
{
    private readonly IImageService _service;

    public ImageController(IImageService service)
    {
        _service = service;
    }

    [HttpGet("Artist/{id}")]
    public async Task Artist(int id)
    {
        var artwork = await _service.GetArtistImage(id);
        Response.ContentType = artwork.MimeType;
        Response.ContentLength = artwork.Bytes.Length;
        var readOnlyMemory = new ReadOnlyMemory<byte>(artwork.Bytes);
        await Response.BodyWriter.WriteAsync(readOnlyMemory);
    }

    [HttpGet("Album/{id}")]
    public async Task Album(int id)
    {
        var artwork = await _service.GetAlbumArtwork(id);
        Response.ContentType = artwork.MimeType;
        Response.ContentLength = artwork.Bytes.Length;
        var readOnlyMemory = new ReadOnlyMemory<byte>(artwork.Bytes);
        await Response.BodyWriter.WriteAsync(readOnlyMemory);
    }
}
