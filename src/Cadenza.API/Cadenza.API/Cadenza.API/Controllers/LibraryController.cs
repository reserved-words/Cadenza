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
    public async Task<AlbumDetailsDTO> Album(int id)
    {
        return await _service.Album(id);
    }

    [HttpGet("Album/Tracks/{id}")]
    public async Task<AlbumTracksDTO> AlbumTracks(int id)
    {
        return await _service.AlbumTracks(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<ArtistDetailsDTO> Artist(int id)
    {
        return await _service.Artist(id);
    }

    [HttpGet("Artist/Albums/{id}")]
    public async Task<List<AlbumDTO>> ArtistAlbums(int id)
    {
        return await _service.ArtistAlbums(id);
    }

    [HttpGet("Artist/AlbumsFeaturing/{id}")]
    public async Task<List<AlbumDTO>> AlbumsFeaturingArtist(int id)
    {
        return await _service.AlbumsFeaturingArtist(id);
    }

    [HttpGet("Artist/Tracks/{id}")]
    public async Task<List<TrackDTO>> ArtistTracks(int id)
    {
        return await _service.ArtistTracks(id);
    }

    [HttpGet("Artists")]
    public async Task<List<ArtistDTO>> Artists()
    {
        return await _service.Artists();
    }

    [HttpGet("Artists/Genre/{id}")]
    public async Task<List<ArtistDTO>> GenreArtists(string id)
    {
        return await _service.GenreArtists(id);
    }

    [HttpGet("Artists/Grouping/{id}")]
    public async Task<List<ArtistDTO>> GroupingArtists(int id)
    {
        return await _service.GroupingArtists(id);
    }

    [HttpGet("Tag/{id}")]
    public async Task<List<PlayerItemDTO>> Tag(string id)
    {
        return await _service.Tag(id);
    }

    [HttpGet("Track/{id}")]
    public async Task<TrackFullDTO> Track(int id)
    {
        return await _service.Track(id);
    }
}
