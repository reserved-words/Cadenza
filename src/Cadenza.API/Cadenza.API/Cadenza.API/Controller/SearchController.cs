using Cadenza.API.Common.Interfaces.Cache;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchCache _searchItems;

    public SearchController(ISearchCache searchItems)
    {
        _searchItems = searchItems;
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Albums()
    {
        return await _searchItems.GetSearchAlbums();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Artists()
    {
        return await _searchItems.GetSearchTracks();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Groupings()
    {
        return await _searchItems.GetSearchGroupings();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Genres()
    {
        return await _searchItems.GetSearchGenres();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Playlists()
    {
        return await _searchItems.GetSearchPlaylists();
    }

    [HttpGet]
    public async Task<List<PlayerItem>> Tracks()
    {
        return await _searchItems.GetSearchTracks();
    }
}
