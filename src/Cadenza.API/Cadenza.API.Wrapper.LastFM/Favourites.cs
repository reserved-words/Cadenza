using Cadenza.API.Core;
using Cadenza.API.Core.LastFM;
using Cadenza.API.Wrapper.Core;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Web;

namespace Cadenza.API.Wrapper.LastFM;

internal class Favourites : IFavourites
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;

    public Favourites(IUrl url, IHttpHelper http)
    {
        _url = url;
        _http = http;
    }

    public async Task Favourite(Track track)
    {
        var url = _url.Build(ApiEndpoints.LastFm.Favourite);
        await _http.Post(url, null, track);
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        var url = _url.Build(ApiEndpoints.LastFm.IsFavourite, ("artist", artist), ("title", title));
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task Unfavourite(Track track)
    {
        var url = _url.Build(ApiEndpoints.LastFm.Unfavourite);
        await _http.Post(url, null, track);
    }
}
