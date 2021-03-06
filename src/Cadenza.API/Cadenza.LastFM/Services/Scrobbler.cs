using Cadenza.API.Core.LastFM;
using Cadenza.LastFM.Interfaces;

namespace Cadenza.LastFM.Services;

public class Scrobbler : IScrobbler
{
    private readonly ILastFmAuthorisedClient _client;

    public Scrobbler(ILastFmAuthorisedClient client)
    {
        _client = client;
    }

    public async Task UpdateNowPlaying(Scrobble scrobble)
    {
        await _client.Post(scrobble.SessionKey, new Dictionary<string, string>
        {
            { "method", "track.updateNowPlaying" },
            { "artist", scrobble.Artist },
            { "track", scrobble.Title },
            { "album", scrobble.AlbumTitle },
            { "albumArtist", scrobble.AlbumArtist },
            { "duration", scrobble.Duration.ToString() },
        });
    }

    public async Task RecordPlay(Scrobble scrobble)
    {
        var scrobbleTime = scrobble.Timestamp.ToUniversalTime();
        var unixTimeStamp = scrobbleTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        await _client.Post(scrobble.SessionKey, new Dictionary<string, string>
        {
            { "method", "track.scrobble" },
            { "artist", scrobble.Artist },
            { "track", scrobble.Title },
            { "album", scrobble.AlbumTitle },
            { "albumArtist", scrobble.AlbumArtist },
            { "timestamp", unixTimeStamp.ToString() },
        });
    }
}