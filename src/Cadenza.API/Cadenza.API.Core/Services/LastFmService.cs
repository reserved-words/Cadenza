using LFM_Track = Cadenza.Common.Domain.Model.LastFm.LFM_Track;

namespace Cadenza.API.Core.Services;

internal class LastFmService : ILastFmService
{
    private readonly IAuthoriser _authoriser;
    private readonly IFavourites _favourites;
    private readonly IHistory _history;
    private readonly IScrobbler _scrobbler;

    public LastFmService(IAuthoriser authoriser, IScrobbler scrobbler, IFavourites favourites, IHistory history)
    {
        _authoriser = authoriser;
        _scrobbler = scrobbler;
        _favourites = favourites;
        _history = history;
    }

    public async Task<string> AuthUrl(string redirectUri)
    {
        return await _authoriser.GetAuthUrl(redirectUri);
    }

    public async Task<string> CreateSession(string token)
    {
        return await _authoriser.CreateSession(token);
    }

    public async Task RecordPlay(LFM_Scrobble scrobble)
    {
        await _scrobbler.RecordPlay(scrobble);
    }

    public async Task UpdateNowPlaying(LFM_Scrobble scrobble)
    {
        await _scrobbler.UpdateNowPlaying(scrobble);
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        return await _favourites.IsFavourite(artist, title);
    }

    public async Task Favourite(LFM_Track track)
    {
        await _favourites.Favourite(track);
    }

    public async Task Unfavourite(LFM_Track track)
    {
        await _favourites.Unfavourite(track);
    }

    public async Task<List<RecentTrack>> RecentTracks(int limit, int page)
    {
        return await _history.GetRecentTracks(limit, page);
    }

    public async Task<List<PlayedAlbum>> TopAlbums(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedAlbums(period, limit, page);
    }

    public async Task<List<PlayedArtist>> TopArtists(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedArtists(period, limit, page);
    }

    public async Task<List<PlayedTrack>> TopTracks(HistoryPeriod period, int limit, int page)
    {
        return await _history.GetPlayedTracks(period, limit, page);
    }
}
