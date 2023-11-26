using Cadenza.Common.LastFm.Model;

namespace Cadenza.Common.LastFm.Implementations;

internal class LastFmService : ILastFmService
{
    private readonly IAuthorisedApiClient _authorisedClient;

    public LastFmService(IAuthorisedApiClient client)
    {
        _authorisedClient = client;
    }

    public async Task ScrobbleTrack(string sessionKey, Scrobble scrobble)
    {
        var scrobbleTime = scrobble.Timestamp.ToUniversalTime();
        var unixTimeStamp = scrobbleTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        await _authorisedClient.Post(sessionKey, new Dictionary<string, string>
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
