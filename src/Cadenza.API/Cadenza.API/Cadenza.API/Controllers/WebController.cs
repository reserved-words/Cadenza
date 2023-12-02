using Cadenza.Common.LastFm;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebController : ControllerBase
{
    private readonly ILastFmInfoService _service;

    public WebController(ILastFmInfoService service)
    {
        _service = service;
    }

    [HttpGet("AlbumArtworkUrl")]
    public async Task<AlbumArtworkDTO> AlbumArtworkUrl(string artist, string title)
    {
        var url = await _service.AlbumArtworkUrl(artist, title);
        return new AlbumArtworkDTO { Url = url };
    }
}
