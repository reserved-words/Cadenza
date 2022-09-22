using Cadenza.API.LastFM;
using Cadenza.API.LastFM.Interfaces;
using Cadenza.API.LastFM.Model;
using Microsoft.Extensions.Options;

namespace Cadenza.API.LastFM.Services;

public class Favourites : IFavourites
{
    private readonly ILastFmAuthorisedClient _authorisedClient;
    private readonly ILastFmClient _client;
    private readonly IOptions<LastFmSettings> _config;

    public Favourites(ILastFmAuthorisedClient authorisedClient, ILastFmClient client, IOptions<LastFmSettings> config)
    {
        _authorisedClient = authorisedClient;
        _client = client;
        _config = config;
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        var url = _config.Value.ApiBaseUrl
            .SetMethod("track.getInfo")
            .Add("artist", artist)
            .Add("track", title)
            .Add("username", _config.Value.Username);

        return await _client.Get(url, xml => xml
            .Element("track")
            .Element("userloved")
            .Value == "1");
    }

    public async Task Favourite(Track track)
    {
        await _authorisedClient.Post(track.SessionKey, new Dictionary<string, string>
        {
            { "method", "track.love" },
            { "track", track.Title },
            { "artist", track.Artist }
        });
    }

    public async Task Unfavourite(Track track)
    {
        await _authorisedClient.Post(track.SessionKey, new Dictionary<string, string>
        {
            { "method", "track.unlove" },
            { "track", track.Title },
            { "artist", track.Artist }
        });
    }
}
