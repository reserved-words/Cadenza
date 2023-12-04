namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous] // Override authentication for now - later can improve this
public class ImageController : ControllerBase
{
    private readonly IImageRepository _repository;

    public ImageController(IImageRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("Artist/{id}")]
    public async Task Artist(int id)
    {
        var image = await _repository.GetArtistImage(id);
        var artwork = image ?? GetDefaultImage();
        Response.ContentType = artwork.MimeType;
        Response.ContentLength = artwork.Bytes.Length;
        var readOnlyMemory = new ReadOnlyMemory<byte>(artwork.Bytes);
        await Response.BodyWriter.WriteAsync(readOnlyMemory);
    }

    [HttpGet("Album/{id}")]
    public async Task Album(int id)
    {
        var image = await _repository.GetAlbumArtwork(id);
        var artwork = image ?? GetDefaultImage();
        Response.ContentType = artwork.MimeType;
        Response.ContentLength = artwork.Bytes.Length;
        var readOnlyMemory = new ReadOnlyMemory<byte>(artwork.Bytes);
        await Response.BodyWriter.WriteAsync(readOnlyMemory);
    }

    private ArtworkImage GetDefaultImage()
    {
        var bytes = System.IO.File.ReadAllBytes("Images/default.png");
        return new ArtworkImage(bytes, "image/png");
    }
}
