namespace Cadenza.Web.Database.Repositories;

internal class HistoryRepository : IHistoryRepository, IPlayTracker
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;
    private readonly IUrl _url;

    public HistoryRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper, IUrl url)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
        _url = url;
    }

    public async Task<List<RecentAlbumVM>> GetRecentAlbums(int maxItems)
    {
        var url = $"{_settings.RecentAlbumRequests}/{maxItems}";
        var items = await _apiHelper.Get<List<RecentAlbumDTO>>(url);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<string>> GetRecentTags(int maxItems)
    {
        var url = $"{_settings.RecentTagRequests}/{maxItems}";
        return await _apiHelper.Get<List<string>>(url);
    }

    public async Task<List<RecentTrackVM>> GetRecentTracks(int maxItems)
    {
        var url = $"{_settings.RecentTracks}/{maxItems}";
        return await _apiHelper.Get<List<RecentTrackVM>>(url);
    }

    public async Task RecordPlay(TrackFullVM track, DateTime timestamp)
    {
        var scrobble = GetScrobble(track, timestamp);
        await _apiHelper.Post(_settings.Scrobble, scrobble);
    }

    public async Task UpdateNowPlaying(TrackFullVM track, int secondsRemaining)
    {
        var nowPlaying = GetNowPlaying(track, secondsRemaining);
        await _apiHelper.Post(_settings.UpdateNowPlaying, nowPlaying);
    }

    public async Task<List<TopAlbumVM>> GetTopAlbums(HistoryPeriod period, int maxItems)
    {
        var url = _url.Build(_settings.TopAlbums, ("period", period), ("maxItems", maxItems));
        return await _apiHelper.Get<List<TopAlbumVM>>(url);
    }

    public async Task<List<TopArtistVM>> GetTopArtists(HistoryPeriod period, int maxItems)
    {
        var url = _url.Build(_settings.TopArtists, ("period", period), ("maxItems", maxItems));
        return await _apiHelper.Get<List<TopArtistVM>>(url);
    }

    public async Task<List<TopTrackVM>> GetTopTracks(HistoryPeriod period, int maxItems)
    {
        var url = _url.Build(_settings.TopTracks, ("period", period), ("maxItems", maxItems));
        return await _apiHelper.Get<List<TopTrackVM>>(url);
    }

    private NowPlayingDTO GetNowPlaying(TrackFullVM track, int secondsRemaining)
    {
        return new NowPlayingDTO
        {
            TrackId = track.Id,
            SecondsRemaining = secondsRemaining
        };
    }

    private ScrobbleDTO GetScrobble(TrackFullVM track, DateTime timestamp)
    {
        return new ScrobbleDTO
        {
            Timestamp = timestamp,
            TrackId = track.Id,
        };
    }
}
