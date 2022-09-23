using Cadenza.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;
[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _library;

    public LibraryController(ILibraryService library)
    {
        _library = library;
    }

    [HttpPost]
    public async Task Populate()
    {
        await _library.Populate();
    }

    [HttpGet("/Artists")]
    public async Task<List<Artist>> Artists()
    {
        return await _library.GetAllArtists();
    }

    [HttpGet("/Artists/Album")]
    public async Task<List<Artist>> AlbumArtists()
    {
        return await _library.GetAlbumArtists();
    }

    [HttpGet("/Artists/Track")]
    public async Task<List<Artist>> TrackArtists()
    {
        return await _library.GetTrackArtists();
    }

    [HttpGet("/Artists/Grouping")]
    public async Task<List<Artist>> GroupingArtists(Grouping id)
    {
        return await _library.GetArtistsByGrouping(id);
    }

    [HttpGet("/Artists/Genre")]
    public async Task<List<Artist>> GenreArtists(string id)
    {
        return await _library.GetArtistsByGenre(id);
    }

    [HttpGet("/Artist")]
    public async Task<ArtistInfo> Artist(string id)
    {
        return await _library.GetArtist(id);
    }

    [HttpGet("/Artist/Albums")]
    public async Task<List<Album>> ArtistAlbums(string id)
    {
        return await _library.GetAlbums(id);
    }

    [HttpGet("/Track")]
    public async Task<TrackFull> Track(string id)
    {
        return await _library.GetTrack(id);
    }

    [HttpGet("/Album")]
    public async Task<AlbumInfo> Album(string id)
    {
        return await _library.GetAlbum(id);
    }

    [HttpGet("/Album/Tracks")]
    public async Task<List<AlbumTrack>> AlbumTracks(string id)
    {
        return await _library.GetTracks(id);
    }
}
