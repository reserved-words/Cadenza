namespace Cadenza.API.LastFM;

internal class Scrobbler : IScrobbler
{
    private readonly IAuthorisedApiClient _client;

    public Scrobbler(IAuthorisedApiClient client)
    {
        _client = client;
    }

    public async Task UpdateNowPlaying(ScrobbleDTO scrobble)
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
}