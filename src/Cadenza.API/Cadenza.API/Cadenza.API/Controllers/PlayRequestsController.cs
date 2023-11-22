namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayRequestsController : ControllerBase
{
    private readonly IHistoryRepository _repository;

    public PlayRequestsController(IHistoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("RecentAlbums/{maxItems}")]
    public async Task<List<RecentAlbumDTO>> RecentAlbums(int maxItems)
    {
        return await _repository.GetRecentAlbums(maxItems);
    }

    [HttpGet("RecentTags/{maxItems}")]
    public async Task<List<string>> RecentTags(int maxItems)
    {
        return await _repository.GetRecentTags(maxItems);
    }

    [HttpPost("LogAlbum/{albumId}")]
    public async Task LogAlbumPlay(int albumId)
    {
        await _repository.LogAlbumPlay(albumId);
    }

    [HttpPost("LogArtist/{artistId}")]
    public async Task LogArtistPlay(int artistId)
    {
        await _repository.LogArtistPlay(artistId);
    }

    [HttpPost("LogGenre/{genre}")]
    public async Task LogGenrePlay(string genre)
    {
        await _repository.LogGenrePlay(genre);
    }

    [HttpPost("LogGrouping/{groupingId}")]
    public async Task LogGroupingPlay(int groupingId)
    {
        await _repository.LogGroupingPlay(groupingId);
    }

    [HttpPost("LogLibrary")]
    public async Task LogLibraryPlay()
    {
        await _repository.LogLibraryPlay();
    }

    [HttpPost("LogTag/{tag}")]
    public async Task LogTagPlay(string tag)
    {
        await _repository.LogTagPlay(tag);
    }

    [HttpPost("LogTrack/{trackId}")]
    public async Task LogTrackPlay(int trackId)
    {
        await _repository.LogTrackPlay(trackId);
    }
}
