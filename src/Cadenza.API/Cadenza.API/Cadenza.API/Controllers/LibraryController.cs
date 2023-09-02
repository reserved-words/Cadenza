namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _service;

    public LibraryController(ILibraryService service)
    {
        _service = service;
    }

    [HttpGet("Album/{id}")]
    public async Task<AlbumInfo> Album(int id)
    {
        return await _service.Album(id);
    }

    [HttpGet("Album/Tracks/{id}")]
    public async Task<List<AlbumTrack>> AlbumTracks(int id)
    {
        return await _service.AlbumTracks(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<ArtistInfo> Artist(int id)
    {
        return await _service.Artist(id);
    }

    [HttpGet("Artist/Albums/{id}")]
    public async Task<List<Album>> ArtistAlbums(int id)
    {
        return await _service.ArtistAlbums(id);
    }

    [HttpGet("Artist/AlbumsFeaturing/{id}")]
    public async Task<List<Album>> AlbumsFeaturingArtist(int id)
    {
        return await _service.AlbumsFeaturingArtist(id);
    }

    [HttpGet("Artist/Tracks/{id}")]
    public async Task<List<Track>> ArtistTracks(int id)
    {
        return await _service.ArtistTracks(id);
    }

    [HttpGet("Artists")]
    public async Task<List<Artist>> Artists()
    {
        return await _service.Artists();
    }

    [HttpGet("Artists/Genre/{id}")]
    public async Task<List<Artist>> GenreArtists(string id)
    {
        return await _service.GenreArtists(id);
    }

    [HttpGet("Artists/Grouping/{id}")]
    public async Task<List<Artist>> GroupingArtists(int id)
    {
        return await _service.GroupingArtists(id);
    }

    [HttpGet("Tag/{id}")]
    public async Task<List<PlayerItem>> Tag(string id)
    {
        return await _service.Tag(id);
    }

    [HttpGet("Track/{id}")]
    public async Task<TrackFull> Track(int id)
    {
        return await _service.Track(id);
    }
}
