namespace Cadenza.LastFM;

public class FavouritesController
{
    private readonly ILastFmAuthorisedClient _client;

    public FavouritesController(ILastFmAuthorisedClient client)
    {
        _client = client;
    }

    public async Task Favourite(Track track)
    {
        await _client.Post(track.SessionKey, new Dictionary<string, string>
            {
                { "method", "track.love" },
                { "track", track.Title },
                { "artist", track.Artist }
            });
    }

    public async Task Unfavourite(Track track)
    {
        await _client.Post(track.SessionKey, new Dictionary<string, string> {
                { "method", "track.unlove" },
                { "track", track.Title },
                { "artist", track.Artist }
            });
    }
}
