namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IHistoryService _service;

    public HistoryController(IHistoryService service)
    {
        _service = service;
    }

    [HttpGet("GetRecentAlbums/{maxItems}")]
    public async Task<List<RecentAlbumDTO>> GetRecentAlbums(int maxItems)
    {
        return await _service.GetRecentAlbums(maxItems);
    }

    [HttpGet("GetRecentTags/{maxItems}")]
    public async Task<List<string>> GetRecentTags(int maxItems)
    {
        return await _service.GetRecentTags(maxItems);
    }

    [HttpPost("LogAlbum/{albumId}")]
    public async Task LogAlbumPlay(int albumId)
    {
        await _service.LogAlbumPlay(albumId);
    }

    [HttpPost("LogArtist/{artistId}")]
    public async Task LogArtistPlay(int artistId)
    {
        await _service.LogArtistPlay(artistId);
    }

    [HttpPost("LogGenre/{genre}")]
    public async Task LogGenrePlay(string genre)
    {
        await _service.LogGenrePlay(genre);
    }

    [HttpPost("LogGrouping/{groupingId}")]
    public async Task LogGroupingPlay(int groupingId)
    {
        await _service.LogGroupingPlay(groupingId);
    }

    [HttpPost("LogLibrary")]
    public async Task LogLibraryPlay()
    {
        await _service.LogLibraryPlay();
    }

    [HttpPost("LogTag/{tag}")]
    public async Task LogTagPlay(string tag)
    {
        await _service.LogTagPlay(tag);
    }

    [HttpPost("LogTrack/{trackId}")]
    public async Task LogTrackPlay(int trackId)
    {
        await _service.LogTrackPlay(trackId);
    }
}
