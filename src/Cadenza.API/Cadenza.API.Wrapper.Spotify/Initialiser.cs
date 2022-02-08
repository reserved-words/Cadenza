using Cadenza.API.Core;
using Cadenza.API.Wrapper.Core;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.API.Wrapper.Spotify;

internal class Initialiser : IInitialiser
{
    private readonly IOptions<ApiSettings> _settings;
    private readonly IHttpHelper _http;

    public Initialiser(IHttpHelper http, IOptions<ApiSettings> settings)
    {
        _http = http;
        _settings = settings;
    }

    public async Task Populate(string accessToken)
    {
        var url = GetApiEndpoint(ApiEndpoints.Spotify.Populate);
        await _http.Post(url, null, accessToken);
    }

    private string GetApiEndpoint(string endpoint)
    {
        return $"{_settings.Value.BaseUrl}{endpoint}";
    }
}
