using Track = Cadenza.Common.Domain.Model.LastFm.Track;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly ILastFmService _service;

    public LastFmController(ILastFmService service)
    {
        _service = service;
    }

    [HttpGet("AuthUrl")]
    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _service.AuthUrl(redirectUri);
    }

    [HttpGet("CreateSession")]
    public async Task<string> CreateSession(string token)
    {
        return await _service.CreateSession(token);
    }

    [HttpPost("RecordPlay")]
    public async Task RecordPlay(Scrobble scrobble)
    {
        await _service.RecordPlay(scrobble);
    }

    [HttpPost("UpdateNowPlaying")]
    public async Task UpdateNowPlaying(Scrobble scrobble)
    {
        await _service.UpdateNowPlaying(scrobble);
    }

    [HttpGet("IsFavourite")]
    public async Task<bool> IsFavourite(string artist, string title)
    {
        return await _service.IsFavourite(artist, title);
    }

    [HttpPost("Favourite")]
    public async Task Favourite(Track track)
    {
        await _service.Favourite(track);
    }

    [HttpPost("Unfavourite")]
    public async Task Unfavourite(Track track)
    {
        await _service.Unfavourite(track);
    }

    [HttpGet("RecentTracks")]
    public async Task<List<RecentTrack>> RecentTracks(int limit, int page)
    {
        return await _service.RecentTracks(limit, page);
    }

    [HttpGet("TopAlbums")]
    public async Task<List<PlayedAlbum>> TopAlbums(HistoryPeriod period, int limit, int page)
    {
        return await _service.TopAlbums(period, limit, page);
    }

    [HttpGet("TopArtists")]
    public async Task<List<PlayedArtist>> TopArtists(HistoryPeriod period, int limit, int page)
    {
        return await _service.TopArtists(period, limit, page);
    }

    [HttpGet("TopTracks")]
    public async Task<List<PlayedTrack>> TopTracks(HistoryPeriod period, int limit, int page)
    {
        return await _service.TopTracks(period, limit, page);
    }
}
