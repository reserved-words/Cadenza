using Cadenza.Common;

namespace Cadenza.API.Spotify;

public class Auth
{
    private readonly IConfigurationSection _config;
    private readonly IBuilder _builder;

    public Auth(IConfiguration config, IBuilder builder)
    {
        _config = config.GetSection("Spotify");
        _builder = builder;
    }

    private string ClientId => _config.GetValue<string>("ClientId");
    private string ClientSecret => _config.GetValue<string>("ClientSecret");
    private string Scopes => _config.GetValue<string>("Scopes");
    private string State => "a1s2d3f4TY63"; // TODO
    private string AuthUri => _config.GetValue<string>("AuthUri");
    private string TokenUri => _config.GetValue<string>("TokenUri");

    public string GetAuthUrl(string redirectUri)
    {
        return _builder.BuildUrl(AuthUri,
            ("response_type", "code"),
            ("client_id", ClientId),
            ("scope", Scopes),
            ("redirect_uri", redirectUri),
            ("state", State));
    }

    public string GetAuthHeader()
    {
        return _builder.BuildAuthHeader(ClientId, ClientSecret);
    }

    public string GetTokenUrl() => TokenUri;
}
