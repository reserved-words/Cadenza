using Cadenza.API.Interfaces.LastFm;
using Cadenza.Common.LastFm;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LastFmController : ControllerBase
{
    private readonly IAuthoriser _authoriser;
    private readonly IFavourites _favourites;
    private readonly IHistory _history;
    private readonly IScrobbler _scrobbler;
    private readonly IHistoryRepository _repository;

    public LastFmController(IAuthoriser authoriser, IScrobbler scrobbler, IFavourites favourites, IHistory history, IHistoryRepository repository)
    {
        _authoriser = authoriser;
        _scrobbler = scrobbler;
        _favourites = favourites;
        _history = history;
        _repository = repository;
    }


    [HttpGet("AuthUrl")]
    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _authoriser.GetAuthUrl(redirectUri);
    }

    [HttpGet("CreateSession")]
    public async Task<string> CreateSession(string token)
    {
        return await _authoriser.CreateSession(token);
    }

    [HttpPost("RecordPlay")]
    public async Task RecordPlay(Scrobble scrobble)
    {
        // TODO: Don't do the actual scrobble here - leave that to the service
        // TODO: Move this out of LastFmController and into HistoryController
        await _repository.ScrobbleTrack(scrobble.TrackId, scrobble.Timestamp);
        await _scrobbler.RecordPlay(scrobble);
    }

    [HttpPost("UpdateNowPlaying")]
    public async Task UpdateNowPlaying(Scrobble scrobble)
    {
        // TODO: Think about letting the service do this as well - eventually no need to have any ref to Last.FM in the API
        await _scrobbler.UpdateNowPlaying(scrobble);
    }

    [HttpGet("IsFavourite")]
    public async Task<bool> IsFavourite(string artist, string title)
    {
        // TODO: Get this from database
        return await _favourites.IsFavourite(artist, title);
    }

    [HttpPost("Favourite")]
    public async Task Favourite(FavouriteTrack track)
    {
        // TODO: Update in database and then service can pass on to Last.FM
        await _favourites.Favourite(track);
    }

    [HttpPost("Unfavourite")]
    public async Task Unfavourite(FavouriteTrack track)
    {
        // TODO: Update in database and then service can pass on to Last.FM
        await _favourites.Unfavourite(track);
    }

    [HttpGet("RecentTracks")]
    public async Task<List<RecentTrackDTO>> RecentTracks(int limit, int page)
    {
        // TODO: Get from database
        return await _history.GetRecentTracks(limit, page);
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
