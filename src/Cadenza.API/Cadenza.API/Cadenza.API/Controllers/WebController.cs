using Cadenza.API.Interfaces.LastFm;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebController : ControllerBase
{
    private readonly IInfoService _service;

    public WebController(IInfoService service)
    {
        _service = service;
    }

    [HttpGet("AlbumArtworkUrl")]
    public async Task<AlbumArtworkDTO> AlbumArtworkUrl(string artist, string title)
    {
        var url = await _service.AlbumArtworkUrl(artist, title);
        return new AlbumArtworkDTO { Url = url };
    }

    [HttpGet("ArtistImageUrl")]
    public Task<ArtistImageDTO> ArtistImageUrl(string name)
    {
        var result = new ArtistImageDTO { Url = null };
        return Task.FromResult(result);
    }
}
