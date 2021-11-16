using Cadenza.Common;

namespace Cadenza.LastFM;

public class Scrobbler : IPlayTracker, IFavouritesConsumer, IFavouritesController
{
    private readonly ILastFmClient _client;

    public Scrobbler(ILastFmClient client)
    {
        _client = client;
    }

    public async Task<bool> IsFavourite(TrackSummary track)
    {
        var url = Api.BaseUrl.SetMethod("track.getInfo")
            .Add("artist", track.Artist.Name)
            .Add("track", track.Track.Title);

        return await _client.Get(url, xml => xml
            .Element("track")
            .Element("userloved")
            .Value == "1");
    }

    public async Task Favourite(TrackSummary track)
    {
        await _client.Post(Api.BaseUrl, new Dictionary<string, string>
            {
                { "method", "track.love" },
                { "track", track.Track.Title },
                { "artist", track.Artist.Name }
            });
    }

    public async Task UpdateNowPlaying(TrackSummary track, int duration)
    {
        await _client.Post(Api.BaseUrl, new Dictionary<string, string>
            {
                { "method", "track.updateNowPlaying" },
                { "artist", track.Artist.Name },
                { "track", track.Track.Title },
                { "album", track.Album.Title },
                { "duration", duration.ToString() },
                { "albumArtist", track.Album.ArtistName }
            });
    }

    public async Task RecordPlay(TrackSummary track, DateTime timestamp)
    {
        var scrobbleTime = timestamp.ToUniversalTime();
        var unixTimeStamp = scrobbleTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        await _client.Post(Api.BaseUrl, new Dictionary<string, string>
            {
                { "method", "track.scrobble" },
                { "artist", track.Artist.Name },
                { "track", track.Track.Title },
                { "album", track.Album.Title },
                { "timestamp", unixTimeStamp.ToString() },
                { "albumArtist", track.Album.ArtistName }
            });
    }

    public async Task Unfavourite(TrackSummary track)
    {
        await _client.Post(Api.BaseUrl, new Dictionary<string, string> {
                { "method", "track.unlove" },
                { "track", track.Track.Title },
                { "artist", track.Artist.Name }
            });
    }
}