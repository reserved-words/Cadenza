using Cadenza.API.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;

[Route("[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _service;

    public SearchController(ISearchService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Albums()
    {
        return await _service.GetSearchAlbums();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Artists()
    {
        return await _service.GetSearchArtists();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Groupings()
    {
        return await _service.GetSearchGroupings();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Genres()
    {
        return await _service.GetSearchGenres();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Tracks()
    {
        return await _service.GetSearchTracks();
    }
}
