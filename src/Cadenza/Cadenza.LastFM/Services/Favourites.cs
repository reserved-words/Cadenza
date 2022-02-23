using Cadenza.Core;
using Cadenza.Core.App;
using Cadenza.Core.Interfaces;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.LastFM;

public class Favourites : IFavouritesConsumer, IFavouritesController
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;
    private readonly IStoreGetter _store;
    private readonly LastFmApiSettings _apiSettings;


    public Favourites(IUrl url, IHttpHelper http, IOptions<LastFmApiSettings> apiSettings, IStoreGetter store)
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
        var track = GetTrack(artist, title);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Favourite);
        await _http.Post(url, null, track);
    }

    public async Task Unfavourite(string artist, string title)
    {
        var track = GetTrack(artist, title);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Unfavourite);
        await _http.Post(url, null, track);
    }

    private async Task<Track> GetTrack(string artist, string title)
    {
        var storedSessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);

        return new Track
        {
            SessionKey = storedSessionKey.Value,
            Artist = artist,
            Title = title
        };
    }
}
