using Microsoft.Extensions.Options;

namespace Cadenza.Spotify;

public class Auth
{
    private readonly IOptions<SpotifySettings> _config;
    private readonly IBuilder _builder;

    public Auth(IOptions<SpotifySettings> config, IBuilder builder)
    {
        _config = config;
        _builder = builder;
    }

    private string State => "a1s2d3f4TY63"; // TODO

    public string GetAuthUrl(string redirectUri)
    {
        return _builder.BuildUrl(_config.Value.AuthUri,
            ("response_type", "code"),
            ("client_id", _config.Value.ClientId),
            ("scope", _config.Value.Scopes),
            ("redirect_uri", redirectUri),
            ("state", State));
    }

    public string GetAuthHeader()
    {
        return _builder.BuildAuthHeader(_config.Value.ClientId, _config.Value.ClientSecret);
    }

    public string GetTokenUrl() => _config.Value.TokenUri;
}
