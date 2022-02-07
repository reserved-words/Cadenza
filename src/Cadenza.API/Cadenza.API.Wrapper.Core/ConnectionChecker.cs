using Cadenza.API.Core;
using Cadenza.Utilities;

namespace Cadenza.API.Wrapper.Core;

internal class ConnectionChecker : IConnectionChecker
{
    private readonly IUrl _url;
    private readonly IHttpHelper _http;

    public ConnectionChecker(IUrl url, IHttpHelper http)
    {
        _url = url;
        _http = http;
    }

    public async Task CheckConnection()
    {
        var url = _url.Build(ApiEndpoints.Connect);
        await _http.GetString(url);
    }
}
