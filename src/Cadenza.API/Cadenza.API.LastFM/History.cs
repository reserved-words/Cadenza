using Cadenza.Domain.Model.History;
using Microsoft.Extensions.Options;

namespace Cadenza.API.LastFM
{
    internal class History : IHistory
    {
        private readonly IApiClient _client;
        private readonly IOptions<ApiSettings> _config;
        private readonly IParser _parser;
        private readonly IUrlService _urlService;

        public History(IApiClient client, IOptions<ApiSettings> config, IParser parser, IUrlService urlService)
        {
            _client = client;
            _config = config;
            _parser = parser;
            _urlService = urlService;
        }

        public async Task<List<RecentTrack>> GetRecentTracks(int limit, int page = 1)
        {
            var url = _config.Value.ApiBaseUrl;
            url = _urlService.SetMethod(url, "user.getRecentTracks");
            url = _urlService.AddParameter(url, "user", _config.Value.Username);
            url = _urlService.AddParameter(url, "limit", limit);
            url = _urlService.AddParameter(url, "page", page);
            url = _urlService.AddParameter(url, "extended", 1);

            return await _client.Get(url, xml => xml
                    .Element("recenttracks")
                    .Elements("track")
                    .Select(t =>
                    {
                        var nowPlaying = _parser.GetBool(t, "nowplaying", true);

                        return new RecentTrack
                        {
                            Title = _parser.Get(t, "name"),
                            Artist = _parser.Get(t, "artist", "name"),
                            Album = _parser.Get(t, "album"),
                            IsLoved = _parser.GetBool(t, "loved"),
                            ImageUrl = _parser.GetImage(t),
                            Played = nowPlaying
                                ? DateTime.Now
                                : _parser.GetDateTime(t, "date"),
                            NowPlaying = nowPlaying
                        };
                    })
                    .ToList());
        }

        public async Task<List<PlayedTrack>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1)
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
                    .Select(t => new PlayedTrack
                    {
                        Title = _parser.Get(t, "name"),
                        Artist = _parser.Get(t, "artist", "name"),
                        ImageUrl = _parser.GetImage(t),
                        Plays = _parser.GetInt(t, "playcount"),
                        Rank = _parser.GetInt(t, "rank", true)
                    })
                    .ToList());
        }

        public async Task<List<PlayedAlbum>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1)
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
                .Select(t => new PlayedAlbum
                {
                    Title = _parser.Get(t, "name"),
                    Artist = _parser.Get(t, "artist", "name"),
                    ImageUrl = _parser.GetImage(t),
                    Plays = _parser.GetInt(t, "playcount"),
                    Rank = _parser.GetInt(t, "rank", true)
                })
                .ToList());
        }

        public async Task<List<PlayedArtist>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1)
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
                .Select(t => new PlayedArtist
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
