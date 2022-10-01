namespace Cadenza.API.LastFM;

internal class Favourites : IFavourites
{
    private readonly IAuthorisedApiClient _authorisedClient;
    private readonly IApiClient _client;
    private readonly IOptions<LastFmApiSettings> _config;
    private readonly IUrlService _urlService;

    public Favourites(IAuthorisedApiClient authorisedClient, IApiClient client, IOptions<LastFmApiSettings> config, IUrlService urlService)
    {
        _authorisedClient = authorisedClient;
        _client = client;
        _config = config;
        _urlService = urlService;
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        var url = _config.Value.ApiBaseUrl;
        url = _urlService.SetMethod(url, "track.getInfo");
        url = _urlService.AddParameter(url, "artist", artist);
        url = _urlService.AddParameter(url, "track", title);
        url = _urlService.AddParameter(url, "username", _config.Value.Username);

        return await _client.Get(url, xml =>
        {
            var trackElement = xml.Element("track");

            var lovedElement = trackElement.Elements().FirstOrDefault(e => e.Name == "userloved");

            return lovedElement != null
                ? lovedElement.Value == "1"
                : false;
        });
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
