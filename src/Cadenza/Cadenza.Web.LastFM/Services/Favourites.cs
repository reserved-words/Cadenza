using Cadenza.Common.LastFm;

namespace Cadenza.Web.LastFM.Services;

internal class Favourites : IFavouritesService
{
    private readonly IUrl _url;
    private readonly ILastFmHttpHelper _http;
    private readonly IState<LastFmConnectionState> _lastFmConnectionState;
    private readonly LastFmApiSettings _apiSettings;

    public Favourites(IUrl url, ILastFmHttpHelper http, IOptions<LastFmApiSettings> apiSettings, IState<LastFmConnectionState> lastFmConnectionState)
    {
        _url = url;
        _http = http;
        _apiSettings = apiSettings.Value;
        _lastFmConnectionState = lastFmConnectionState;
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.IsFavourite, ("artist", artist), ("title", title));
        return await _http.Get<bool>(url);
    }

    public async Task Favourite(string artist, string title)
    {
        var track = GetFavouriteTrack(artist, title);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Favourite);
        await _http.Post(url, track);
    }

    public async Task Unfavourite(string artist, string title)
    {
        var track = GetFavouriteTrack(artist, title);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Unfavourite);
        await _http.Post(url, track);
    }

    private FavouriteTrack GetFavouriteTrack(string artist, string title)
    {
        var sessionKey = _lastFmConnectionState.Value.SessionKey;

        return new FavouriteTrack
        {
            SessionKey = sessionKey,
            Artist = artist,
            Title = title
        };
    }
}
