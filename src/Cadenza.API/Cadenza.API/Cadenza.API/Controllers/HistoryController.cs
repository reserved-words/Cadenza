namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IHistoryRepository _repository;

    public HistoryController(IHistoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("RecentlyAddedAlbums/{maxItems}")]
    public async Task<List<RecentAlbumDTO>> RecentlyAddedAlbums(int maxItems)
    {
        return await _repository.GetRecentlyAddedAlbums(maxItems);
    }

    [HttpGet("RecentAlbumRequests/{maxItems}")]
    public async Task<List<RecentAlbumDTO>> RecentAlbumRequests(int maxItems)
    {
        return await _repository.GetRecentlyPlayedAlbums(maxItems);
    }

    [HttpGet("RecentTagRequests/{maxItems}")]
    public async Task<List<string>> RecentTagRequests(int maxItems)
    {
        return await _repository.GetRecentTags(maxItems);
    }

    [HttpGet("RecentTracks/{maxItems}")]
    public async Task<List<RecentTrackDTO>> GetRecentTracks(int maxItems)
    {
        var username = HttpContext.GetUsername();
        return await _repository.GetRecentTracks(username, maxItems);
    }

    [HttpPost("RecordPlay")]
    public async Task RecordPlay(ScrobbleDTO scrobble)
    {
        var username = HttpContext.GetUsername();
        await _repository.ScrobbleTrack(scrobble.TrackId, username, scrobble.Timestamp);
    }

    [HttpPost("UpdateNowPlaying")]
    public async Task UpdateNowPlaying(NowPlayingDTO nowPlaying)
    {
        var username = HttpContext.GetUsername();
        await _repository.UpdateNowPlaying(username, nowPlaying.TrackId, nowPlaying.SecondsRemaining);
    }

    [HttpGet("TopAlbums")]
    public async Task<List<TopAlbumDTO>> TopAlbums(HistoryPeriod period, int maxItems)
    {
        return await _repository.GetTopAlbums(period, maxItems);
    }

    [HttpGet("TopArtists")]
    public async Task<List<TopArtistDTO>> TopArtists(HistoryPeriod period, int maxItems)
    {
        return await _repository.GetTopArtists(period, maxItems);
    }

    [HttpGet("TopTracks")]
    public async Task<List<TopTrackDTO>> TopTracks(HistoryPeriod period, int maxItems)
    {
        return await _repository.GetTopTracks(period, maxItems);
    }
}
