using Cadenza.Core;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Settings;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Spotify.Services;

internal class Initialiser : IInitialiser
{
    private readonly SpotifyApiSettings _settings;
    private readonly IHttpHelper _http;
    private readonly IUrl _url;

    public Initialiser(IHttpHelper http, IOptions<SpotifyApiSettings> settings, IUrl url)
    {
        _http = http;
        _settings = settings.Value;
        _url = url;
    }

    public async Task Populate(string accessToken)
    {

    }
}
