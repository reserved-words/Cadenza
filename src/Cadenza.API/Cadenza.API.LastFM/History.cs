namespace Cadenza.API.LastFM
{
    internal class History : IHistory
    {
        private readonly IApiClient _client;
        private readonly IOptions<LastFmApiSettings> _config;
        private readonly IParser _parser;
        private readonly IUrlService _urlService;

        public History(IApiClient client, IOptions<LastFmApiSettings> config, IParser parser, IUrlService urlService)
        {
            _client = client;
            _config = config;
            _parser = parser;
            _urlService = urlService;
        }

        public async Task<List<PlayedTrackDTO>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1)
        {
            var url = _config.Value.ApiBaseUrl;
            url = _urlService.SetMethod(url, "user.getTopTracks");
            url = _urlService.AddParameter(url, "user", _config.Value.Username);
            url = _urlService.AddParameter(url, "limit", limit);
            url = _urlService.AddParameter(url, "page", page);
            url = _urlService.AddParameter(url, "period", GetPeriod(period));

            return await _client.Get(url, xml => xml
                    .Element("toptracks")
                    .Elements("track")
                    .Select(t => new PlayedTrackDTO
                    {
                        Title = _parser.Get(t, "name"),
                        Artist = _parser.Get(t, "artist", "name"),
                        ImageUrl = _parser.GetImage(t),
                        Plays = _parser.GetInt(t, "playcount"),
                        Rank = _parser.GetInt(t, "rank", true)
                    })
                    .ToList());
        }

        public async Task<List<PlayedAlbumDTO>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1)
        {
            var url = _config.Value.ApiBaseUrl;
            url = _urlService.SetMethod(url, "user.getTopAlbums");
            url = _urlService.AddParameter(url, "user", _config.Value.Username);
            url = _urlService.AddParameter(url, "limit", limit);
            url = _urlService.AddParameter(url, "page", page);
            url = _urlService.AddParameter(url, "period", GetPeriod(period));

            return await _client.Get(url, xml => xml
                .Element("topalbums")
                .Elements("album")
                .Select(t => new PlayedAlbumDTO
                {
                    Title = _parser.Get(t, "name"),
                    Artist = _parser.Get(t, "artist", "name"),
                    ImageUrl = _parser.GetImage(t),
                    Plays = _parser.GetInt(t, "playcount"),
                    Rank = _parser.GetInt(t, "rank", true)
                })
                .ToList());
        }

        public async Task<List<PlayedArtistDTO>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1)
        {
            var url = _config.Value.ApiBaseUrl;
            url = _urlService.SetMethod(url, "user.getTopArtists");
            url = _urlService.AddParameter(url, "user", _config.Value.Username);
            url = _urlService.AddParameter(url, "limit", limit);
            url = _urlService.AddParameter(url, "page", page);
            url = _urlService.AddParameter(url, "period", GetPeriod(period));

            return await _client.Get(url, xml => xml
                .Element("topartists")
                .Elements("artist")
                .Select(t => new PlayedArtistDTO
                {
                    Name = _parser.Get(t, "name"),
                    ImageUrl = _parser.GetImage(t),
                    Plays = _parser.GetInt(t, "playcount"),
                    Rank = _parser.GetInt(t, "rank", true)
                })
                .ToList());
        }

        private static string GetPeriod(HistoryPeriod period)
        {
            return period switch
            {
                HistoryPeriod.Week => "7day",
                HistoryPeriod.Month => "1month",
                HistoryPeriod.QuarterYear => "3month",
                HistoryPeriod.HalfYear => "6month",
                HistoryPeriod.Year => "12month",
                _ => "Overall"
            };
        }
    }
}
