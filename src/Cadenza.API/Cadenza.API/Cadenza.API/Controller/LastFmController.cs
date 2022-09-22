using Cadenza.API.LastFM;
using Cadenza.API.LastFM.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;
[Route("api/[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly IAuthoriser _authoriser;
    private readonly IFavourites _favourites;
    private readonly IHistory _history;
    private readonly IScrobbler _scrobbler;

    public LastFmController(IAuthoriser authoriser, IScrobbler scrobbler, IFavourites favourites, IHistory history)
    {
        _authoriser = authoriser;
        _scrobbler = scrobbler;
        _favourites = favourites;
        _history = history;
    }

    [HttpGet]
    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _authoriser.GetAuthUrl(redirectUri);
    }

    [HttpGet]
    public async Task<string> CreateSession(string token)
    {
        return await _authoriser.CreateSession(token);
    }

    [HttpPost]
    public async Task RecordPlay(Scrobble scrobble)
    {
        await _scrobbler.RecordPlay(scrobble);
    }

    [HttpPost]
    public async Task UpdateNowPlaying(Scrobble scrobble)
    {
        await _scrobbler.UpdateNowPlaying(scrobble);
    }

    [HttpGet]
    public async Task<bool> IsFavourite(string artist, string title)
    {
        return await _favourites.IsFavourite(artist, title);
    }

    [HttpPost]
    public async Task Favourite(LastFM.Track track)
    {
        await _favourites.Favourite(track);
    }

    [HttpPost]
    public async Task Unfavourite(LastFM.Track track)
    {
        await _favourites.Unfavourite(track);
    }

    [HttpGet]
    public async Task<List<RecentTrack>> RecentTracks(int limit, int page)
    {
        return await _history.GetRecentTracks(limit, page);
    }

    [HttpGet]
    public async Task<List<PlayedAlbum>> TopAlbums(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedAlbums(period, limit, page);
    }

    [HttpGet]
    public async Task<List<PlayedArtist>> TopArtists(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedArtists(period, limit, page);
    }

    [HttpGet]
    public async Task<List<PlayedTrack>> TopTracks(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedTracks(period, limit, page);
    }
}
