namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly ILibraryRepository _repository;

    public LibraryController(ILibraryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("Album/{id}")]
    public async Task<AlbumDetailsDTO> Album(int id)
    {
        return await _repository.GetAlbum(id);
    }

    [HttpGet("Album/Full/{id}")]
    public async Task<AlbumFullDTO> AlbumFull(int id)
    {
        return await _repository.GetAlbumFull(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<ArtistDetailsDTO> Artist(int id)
    {
        return await _repository.GetArtist(id);
    }

    [HttpGet("Artist/Albums/{id}")]
    public async Task<List<AlbumDTO>> ArtistAlbums(int id)
    {
        return await _repository.GetArtistAlbums(id);
    }

    [HttpGet("Artist/AlbumsFeaturing/{id}")]
    public async Task<List<AlbumDTO>> AlbumsFeaturingArtist(int id)
    {
        return await _repository.GetAlbumsFeaturingArtist(id);
    }

    [HttpGet("Artists/Genre/{id}")]
    public async Task<List<ArtistDTO>> GenreArtists(string id)
    {
        return await _repository.GetArtistsByGenre(id);
    }

    [HttpGet("Artists/Grouping/{id}")]
    public async Task<List<ArtistDTO>> GroupingArtists(int id)
    {
        return await _repository.GetArtistsByGrouping(id);
    }

    [HttpGet("Tag/{id}")]
    public async Task<List<TaggedItemDTO>> Tag(string id)
    {
        return await _repository.GetTaggedItems(id);
    }

    [HttpGet("Track/{id}")]
    public async Task<TrackDetailsDTO> Track(int id)
    {
        return await _repository.GetTrack(id);
    }

    [HttpGet("Track/Full/{id}")]
    public async Task<TrackFullDTO> TrackFull(int id)
    {
        return await _repository.GetTrackFull(id);
    }
}
