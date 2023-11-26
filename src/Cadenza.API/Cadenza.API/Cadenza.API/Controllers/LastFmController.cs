﻿using Cadenza.API.Extensions;
using Cadenza.API.Interfaces.LastFm;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly IAuthoriser _authoriser;
    private readonly IFavourites _favourites;
    private readonly IHistory _history;
    private readonly IHistoryRepository _historyRepository;
    private readonly IAdminRepository _adminRepository;

    public LastFmController(IAuthoriser authoriser, IFavourites favourites, IHistory history, IHistoryRepository repository, IAdminRepository adminRepository)
    {
        _authoriser = authoriser;
        _favourites = favourites;
        _history = history;
        _historyRepository = repository;
        _adminRepository = adminRepository;
    }


    [HttpGet("AuthUrl")]
    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _authoriser.GetAuthUrl(redirectUri);
    }

    // TODO: Should be POST
    [HttpGet("CreateSession")]
    public async Task<string> CreateSession(string token)
    {
        var session = await _authoriser.CreateSession(token);
        var username = HttpContext.GetUsername();
        await _adminRepository.SaveLastFmSessionKey(username, session.Username, session.SessionKey);
        return session.SessionKey;
    }

    [HttpPost("RecordPlay")]
    public async Task RecordPlay(ScrobbleDTO scrobble)
    {
        // TODO: Move this out of LastFmController and into HistoryController
        var username = HttpContext.GetUsername();
        await _historyRepository.ScrobbleTrack(scrobble.TrackId, username, scrobble.Timestamp);
    }

    [HttpPost("UpdateNowPlaying")]
    public async Task UpdateNowPlaying(NowPlayingDTO nowPlaying)
    {
        // TODO: Move this out of LastFmController and into HistoryController
        var username = HttpContext.GetUsername();
        await _historyRepository.UpdateNowPlaying(username, nowPlaying.TrackId, nowPlaying.SecondsRemaining);
    }

    [HttpGet("IsFavourite")]
    public async Task<bool> IsFavourite(string artist, string title)
    {
        // TODO: Get this from database
        return await _favourites.IsFavourite(artist, title);
    }

    [HttpPost("Favourite")]
    public async Task Favourite(FavouriteTrackDTO track)
    {
        // TODO: Update in database and then service can pass on to Last.FM
        await _favourites.Favourite(track);
    }

    [HttpPost("Unfavourite")]
    public async Task Unfavourite(FavouriteTrackDTO track)
    {
        // TODO: Update in database and then service can pass on to Last.FM
        await _favourites.Unfavourite(track);
    }

    [HttpGet("RecentTracks")]
    public async Task<List<RecentTrackDTO>> RecentTracks(int limit)
    {
        // TODO: Move this out of LastFmController and into HistoryController
        var username = HttpContext.GetUsername();
        return await _historyRepository.GetRecentTracks(username, limit);
    }

    [HttpGet("TopAlbums")]
    public async Task<List<PlayedAlbumDTO>> TopAlbums(HistoryPeriod period, int limit, int page)
    {
        // TODO: Get from database
        return await _history.GetPlayedAlbums(period, limit, page);
    }

    [HttpGet("TopArtists")]
    public async Task<List<PlayedArtistDTO>> TopArtists(HistoryPeriod period, int limit, int page)
    {
        // TODO: Get from database
        return await _history.GetPlayedArtists(period, limit, page);
    }

    [HttpGet("TopTracks")]
    public async Task<List<PlayedTrackDTO>> TopTracks(HistoryPeriod period, int limit, int page)
    {
        // TODO: Get from database
        return await _history.GetPlayedTracks(period, limit, page);
    }
}
