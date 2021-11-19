namespace Cadenza.LastFM;

public class FavouritesConsumer
{
    private readonly ILastFmClient _client;
    private readonly ILastFmConfig _config;

    public FavouritesConsumer(ILastFmClient client, ILastFmConfig config)
    {
        _client = client;
        _config = config;
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        var url = _config.ApiBaseUrl
            .SetMethod("track.getInfo")
            .Add("artist", artist)
            .Add("track", title);

        return await _client.Get(url, xml => xml
            .Element("track")
            .Element("userloved")
            .Value == "1");
    }
}
