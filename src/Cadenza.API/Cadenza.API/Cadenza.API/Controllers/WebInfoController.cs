﻿namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebInfoController : ControllerBase
{
    private readonly IWebInfoService _service;

    public WebInfoController(IWebInfoService service)
    {
        _service = service;
    }

    [HttpGet("AlbumArtworkUrl")]
    public async Task<AlbumArtworkDTO> AlbumArtworkUrl(string artist, string title)
    {
        return await _service.AlbumArtworkUrl(artist, title);
    }

    [HttpGet("ArtistImageUrl")]
    public async Task<ArtistImageDTO> ArtistImageUrl(string name)
    {
        return await _service.ArtistImageUrl(name);
    }
}
