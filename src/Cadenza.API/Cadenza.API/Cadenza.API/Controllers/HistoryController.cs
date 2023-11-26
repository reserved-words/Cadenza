using Cadenza.API.Extensions;
using Cadenza.Database.Interfaces;

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

    [HttpGet("RecentAlbumRequests/{maxItems}")]
    public async Task<List<RecentAlbumDTO>> RecentAlbumRequests(int maxItems)
    {
        return await _repository.GetRecentAlbums(maxItems);
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
}
