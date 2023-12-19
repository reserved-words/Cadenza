using Cadenza.Common;

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
    public async Task<AlbumDTO> Album(int id)
    {
        return await _repository.GetAlbum(id);
    }

    [HttpGet("Album/Full/{id}")]
    public async Task<AlbumFullDTO> AlbumFull(int id)
    {
        return await _repository.GetAlbumFull(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<ArtistDTO> Artist(int id)
    {
        return await _repository.GetArtist(id);
    }

    [HttpGet("Artist/Full")]
    public async Task<ArtistFullDTO> ArtistFull(int id, bool includeAlbumsByOtherArtists)
    {
        return await _repository.GetFullArtist(id, includeAlbumsByOtherArtists);
    }

    [HttpGet("Genre/{genre}")]
    public async Task<GenreDTO> Genre(string genre)
    {
        var split = genre.SplitGenreId();
        return await _repository.GetGenre(split.Grouping, split.Genre);
    }

    [HttpGet("Artists/Grouping/{grouping}")]
    public async Task<List<ArtistDTO>> GroupingArtists(string grouping)
    {
        return await _repository.GetArtistsByGrouping(grouping);
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
