using Cadenza.API.Common.Controllers;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _service;

    public LibraryController(ILibraryService service)
    {
        _service = service;
    }

    [HttpGet("Artists")]
    public async Task<List<Artist>> Artists()
    {
        return await _service.Artists();
    }

    [HttpGet("Artists/Album")]
    public async Task<List<Artist>> AlbumArtists()
    {
        return await _service.AlbumArtists();
    }

    [HttpGet("Artists/Track")]
    public async Task<List<Artist>> TrackArtists()
    {
        return await _service.TrackArtists();
    }

    [HttpGet("Artists/Grouping")]
    public async Task<List<Artist>> GroupingArtists(Grouping id)
    {
        return await _service.GroupingArtists(id);
    }

    [HttpGet("Artists/Genre")]
    public async Task<List<Artist>> GenreArtists(string id)
    {
        return await _service.GenreArtists(id);
    }

    [HttpGet("Artist")]
    public async Task<ArtistInfo> Artist(string id)
    {
        return await _service.Artist(id);
    }

    [HttpGet("Artist/Albums")]
    public async Task<List<Album>> ArtistAlbums(string id)
    {
        return await _service.ArtistAlbums(id);
    }

    [HttpGet("Track")]
    public async Task<TrackFull> Track(string id)
    {
        return await _service.Track(id);
    }

    [HttpGet("Album")]
    public async Task<AlbumInfo> Album(string id)
    {
        return await _service.Album(id);
    }

    [HttpGet("Album/Tracks")]
    public async Task<List<AlbumTrack>> AlbumTracks(string id)
    {
        return await _service.AlbumTracks(id);
    }
}
