using Cadenza.API.Common.Controllers;
using Cadenza.API.LastFM;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;
[Route("[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly ILastFmService _service;

    public LastFmController(ILastFmService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _service.AuthUrl(redirectUri);
    }

    [HttpGet]
    public async Task<string> CreateSession(string token)
    {
        return await _service.CreateSession(token);
    }

    [HttpPost]
    public async Task RecordPlay(Scrobble scrobble)
    {
        await _service.RecordPlay(scrobble);
    }

    [HttpPost]
    public async Task UpdateNowPlaying(Scrobble scrobble)
    {
        await _service.UpdateNowPlaying(scrobble);
    }

    [HttpGet]
    public async Task<bool> IsFavourite(string artist, string title)
    {
        return await _service.IsFavourite(artist, title);
    }

    [HttpPost]
    public async Task Favourite(LastFM.Track track)
    {
        await _service.Favourite(track);
    }

    [HttpPost]
    public async Task Unfavourite(LastFM.Track track)
    {
        await _service.Unfavourite(track);
    }

    [HttpGet]
    public async Task<List<RecentTrack>> RecentTracks(int limit, int page)
    {
        return await _service.RecentTracks(limit, page);
    }

    [HttpGet]
    public async Task<List<PlayedAlbum>> TopAlbums(HistoryPeriod period, int limit, int page)
    {
        return await _service.TopAlbums(period, limit, page);
    }

    [HttpGet]
    public async Task<List<PlayedArtist>> TopArtists(HistoryPeriod period, int limit, int page)
    {
        return await _service.TopArtists(period, limit, page);
    }

    [HttpGet]
    public async Task<List<PlayedTrack>> TopTracks(HistoryPeriod period, int limit, int page)
    {
        return await _service.TopTracks(period, limit, page);
    }
}
