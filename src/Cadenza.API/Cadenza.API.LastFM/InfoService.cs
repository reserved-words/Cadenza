namespace Cadenza.API.LastFM;

internal class InfoService : IInfoService
{
    private readonly IApiClient _client;
    private readonly IOptions<LastFmApiSettings> _config;
    private readonly IParser _parser;
    private readonly IUrlService _urlService;

    public InfoService(IApiClient client, IOptions<LastFmApiSettings> config, IUrlService urlService, IParser parser)
    {
        _client = client;
        _config = config;
        _urlService = urlService;
        _parser = parser;
    }

    public async Task<string> AlbumArtworkUrl(string artist, string title)
    {
        var url = _config.Value.ApiBaseUrl;
        url = _urlService.SetMethod(url, "album.getInfo");
        url = _urlService.AddParameter(url, "artist", artist);
        url = _urlService.AddParameter(url, "album", title);
        url = _urlService.AddParameter(url, "autocorrect", 1);

        return await _client.Get(url, xml =>
        {
            var albumElement = xml.Element("album");
            return _parser.GetImage(albumElement);
        });
    }
}
