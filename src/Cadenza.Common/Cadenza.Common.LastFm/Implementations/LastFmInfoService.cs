namespace Cadenza.Common.LastFm.Implementations;

internal class LastFmInfoService : ILastFmInfoService
{
    private readonly IApiClient _client;
    private readonly IParser _parser;

    public LastFmInfoService(IApiClient client, IParser parser)
    {
        _client = client;
        _parser = parser;
    }

    public async Task<string> AlbumArtworkUrl(string artist, string title)
    {
        var parameters = new Dictionary<string, string> 
        {
            { "method", "album.getInfo" },
            { "artist", artist },
            { "album", title },
            { "autocorrect", "1" }
        };

        return await _client.Get(parameters, xml =>
        {
            var albumElement = xml.Element("album");
            return _parser.GetImage(albumElement);
        });
    }
}
