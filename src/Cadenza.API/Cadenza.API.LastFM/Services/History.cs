using Cadenza.API.LastFM;
using Cadenza.API.LastFM.Interfaces;
using Cadenza.API.LastFM.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.History;
using Microsoft.Extensions.Options;

namespace Cadenza.API.LastFM.Services
{
    public class History : IHistory
    {
        private readonly ILastFmClient _client;
        private readonly IOptions<LastFmSettings> _config;

        public History(ILastFmClient client, IOptions<LastFmSettings> config)
        {
            _client = client;
            _config = config;
        }

        public async Task<List<RecentTrack>> GetRecentTracks(int limit, int page = 1)
        {
            var url = _config.Value.ApiBaseUrl
                .SetMethod("user.getRecentTracks")
                .Add("user", _config.Value.Username)
                .Add("limit", limit)
                .Add("page", page)
                .Add("extended", 1);

            return await _client.Get(url, xml => xml
                    .Element("recenttracks")
                    .Elements("track")
                    .Select(t =>
                    {
                        var nowPlaying = t.GetBool("nowplaying", true);

                        return new RecentTrack
                        {
                            Title = t.Get("name"),
                            Artist = t.Get("artist", "name"),
                            Album = t.Get("album"),
                            IsLoved = t.GetBool("loved"),
                            ImageUrl = t.GetImage(),
                            Played = nowPlaying
                                ? DateTime.Now
                                : t.GetDateTime("date", true),
                            NowPlaying = nowPlaying
                        };
                    })
                    .ToList());
        }

        public async Task<List<PlayedTrack>> GetPlayedTracks(HistoryPeriod period, int limit, int page = 1)
        {
            var url = _config.Value.ApiBaseUrl
                .SetMethod("user.getTopTracks")
                .Add("user", _config.Value.Username)
                .Add("limit", limit)
                .Add("page", page)
                .Add("period", GetPeriod(period));

            return await _client.Get(url, xml => xml
                    .Element("toptracks")
                    .Elements("track")
                    .Select(t => new PlayedTrack
                    {
                        Title = t.Get("name"),
                        Artist = t.Get("artist", "name"),
                        ImageUrl = t.GetImage(),
                        Plays = t.GetInt("playcount"),
                        Rank = t.GetInt("rank", true)
                    })
                    .ToList());
        }

        public async Task<List<PlayedAlbum>> GetPlayedAlbums(HistoryPeriod period, int limit, int page = 1)
        {
            var url = _config.Value.ApiBaseUrl
                .SetMethod("user.getTopAlbums")
                .Add("user", _config.Value.Username)
                .Add("limit", limit)
                .Add("page", page)
                .Add("period", GetPeriod(period));

            return await _client.Get(url, xml => xml
                .Element("topalbums")
                .Elements("album")
                .Select(t => new PlayedAlbum
                {
                    Title = t.Get("name"),
                    Artist = t.Get("artist", "name"),
                    ImageUrl = t.GetImage(),
                    Plays = t.GetInt("playcount"),
                    Rank = t.GetInt("rank", true)
                })
                .ToList());
        }

        public async Task<List<PlayedArtist>> GetPlayedArtists(HistoryPeriod period, int limit, int page = 1)
        {
            var url = _config.Value.ApiBaseUrl
                .SetMethod("user.getTopArtists")
                .Add("user", _config.Value.Username)
                .Add("limit", limit)
                .Add("page", page)
                .Add("period", GetPeriod(period));

            return await _client.Get(url, xml => xml
                .Element("topartists")
                .Elements("artist")
                .Select(t => new PlayedArtist
                {
                    Name = t.Get("name"),
                    ImageUrl = t.GetImage(),
                    Plays = t.GetInt("playcount"),
                    Rank = t.GetInt("rank", true)
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
