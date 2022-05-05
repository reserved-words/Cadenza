using Cadenza.Spotify.Interfaces;
using Microsoft.Extensions.Options;
namespace Cadenza.Spotify;



public class Authoriser : IAuthoriser
{
    private readonly IOptions<SpotifySettings> _config;
    private readonly IBuilder _builder;
    public Authoriser(IOptions<SpotifySettings> config, IBuilder builder)
    {
        _config = config;
        _builder = builder;
    }

    public Task<string> GetAuthUrl(string state, string redirectUri)
    {
        var result = _builder.BuildUrl(_config.Value.AuthUri,
            ("response_type", "code"),
            ("client_id", _config.Value.ClientId),
            ("scope", _config.Value.Scopes),
            ("redirect_uri", redirectUri),
            ("state", state));
        return Task.FromResult(result);
    }

    public Task<string> GetAuthHeader()
    {
        var result = _builder.BuildAuthHeader(_config.Value.ClientId, _config.Value.ClientSecret);
        return Task.FromResult(result);
    }
}
