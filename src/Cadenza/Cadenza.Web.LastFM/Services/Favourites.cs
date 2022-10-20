using Cadenza.Web.Common.Interfaces.Favourites;
using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.LastFM.Services;

internal class Favourites : IFavouritesMessenger, IFavouritesController
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;
    private readonly IAppStore _store;
    private readonly LastFmApiSettings _apiSettings;

    public Favourites(IUrl url, IHttpHelper http, IOptions<LastFmApiSettings> apiSettings, IAppStore store)
    {
        _url = url;
        _http = http;
        _apiSettings = apiSettings.Value;
        _store = store;
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.IsFavourite, ("artist", artist), ("title", title));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task Favourite(string artist, string title)
    {
        var track = await GetTrack(artist, title);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Favourite);
        await _http.Post(url, null, track);
    }

    public async Task Unfavourite(string artist, string title)
    {
        var track = await GetTrack(artist, title);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Unfavourite);
        await _http.Post(url, null, track);
    }

    private async Task<LFM_Track> GetTrack(string artist, string title)
    {
        var storedSessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);

        return new LFM_Track
        {
            SessionKey = storedSessionKey.Value,
            Artist = artist,
            Title = title
        };
    }
}
