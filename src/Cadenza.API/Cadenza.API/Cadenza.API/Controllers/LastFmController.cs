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

    public LastFmController(IAuthoriser authoriser, IScrobbler scrobbler, IFavourites favourites, IHistory history)
    {
        _authoriser = authoriser;
        _scrobbler = scrobbler;
        _favourites = favourites;
        _history = history;
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
        await _scrobbler.RecordPlay(scrobble);
    }

    [HttpPost("UpdateNowPlaying")]
    public async Task UpdateNowPlaying(Scrobble scrobble)
    {
        await _scrobbler.UpdateNowPlaying(scrobble);
    }

    [HttpGet("IsFavourite")]
    public async Task<bool> IsFavourite(string artist, string title)
    {
        return await _favourites.IsFavourite(artist, title);
    }

    [HttpPost("Favourite")]
    public async Task Favourite(FavouriteTrack track)
    {
        await _favourites.Favourite(track);
    }

    [HttpPost("Unfavourite")]
    public async Task Unfavourite(FavouriteTrack track)
    {
        await _favourites.Unfavourite(track);
    }

    [HttpGet("RecentTracks")]
    public async Task<List<RecentTrackDTO>> RecentTracks(int limit, int page)
    {
        return await _history.GetRecentTracks(limit, page);
    }

    [HttpGet("TopAlbums")]
    public async Task<List<PlayedAlbumDTO>> TopAlbums(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedAlbums(period, limit, page);
    }

    [HttpGet("TopArtists")]
    public async Task<List<PlayedArtistDTO>> TopArtists(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedArtists(period, limit, page);
    }

    [HttpGet("TopTracks")]
    public async Task<List<PlayedTrackDTO>> TopTracks(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedTracks(period, limit, page);
    }
}
