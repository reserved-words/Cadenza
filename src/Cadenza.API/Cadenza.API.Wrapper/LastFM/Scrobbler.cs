using Cadenza.API.Core;
using Cadenza.API.Core.LastFM;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Web;

namespace Cadenza.API.Wrapper.LastFM;

internal class Scrobbler : IScrobbler
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;

    public Scrobbler(IUrl url, IHttpHelper http)
    {
        _url = url;
        _http = http;
    }

    public async Task RecordPlay(Scrobble scrobble)
    {
        var url = _url.Build(ApiEndpoints.Scrobble);
        await _http.Post(url, null, scrobble);
    }

    public async Task UpdateNowPlaying(Scrobble scrobble)
    {
        var url = _url.Build(ApiEndpoints.UpdateNowPlaying);
        await _http.Post(url, null, scrobble);
    }
}
