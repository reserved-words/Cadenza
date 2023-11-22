using Cadenza.API.Interfaces.Services;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly ILibraryCache _cache;
    private readonly ICachePopulater _populater;

    public LibraryController(ILibraryCache cache, ICachePopulater populater)
    {
        _cache = cache;
        _populater = populater;
    }

    [HttpGet("Album/{id}")]
    public async Task<AlbumDetailsDTO> Album(int id)
    {
        await PopulateCache();
        return await _cache.Albums.GetAlbum(id);
    }

    [HttpGet("Album/Tracks/{id}")]
    public async Task<AlbumTracksDTO> AlbumTracks(int id)
    {
        await PopulateCache();
        return await _cache.Albums.GetAlbumTracks(id);
    }

    [HttpGet("Artist/{id}")]
    public async Task<ArtistDetailsDTO> Artist(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtist(id);
    }

    [HttpGet("Artist/Albums/{id}")]
    public async Task<List<AlbumDTO>> ArtistAlbums(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetAlbums(id);
    }

    [HttpGet("Artist/AlbumsFeaturing/{id}")]
    public async Task<List<AlbumDTO>> AlbumsFeaturingArtist(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetAlbumsFeaturingArtist(id);
    }

    [HttpGet("Artist/Tracks/{id}")]
    public async Task<List<TrackDTO>> ArtistTracks(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistTracks(id);
    }

    [HttpGet("Artists")]
    public async Task<List<ArtistDTO>> Artists()
    {
        await PopulateCache();
        return await _cache.Artists.GetAllArtists();
    }

    [HttpGet("Artists/Genre/{id}")]
    public async Task<List<ArtistDTO>> GenreArtists(string id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistsByGenre(id);
    }

    [HttpGet("Artists/Grouping/{id}")]
    public async Task<List<ArtistDTO>> GroupingArtists(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistsByGrouping(id);
    }

    [HttpGet("Tag/{id}")]
    public async Task<List<PlayerItemDTO>> Tag(string id)
    {
        await PopulateCache();
        return await _cache.Tags.GetTag(id);
    }

    [HttpGet("Track/{id}")]
    public async Task<TrackFullDTO> Track(int id)
    {
        await PopulateCache();
        return await _cache.Tracks.GetTrack(id);
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
