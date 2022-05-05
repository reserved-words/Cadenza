using Cadenza.Core;

namespace Cadenza.Source.Spotify.Settings;

public class SpotifyApiSettings : ApiOptions<SpotifyApiEndpoints>
{
    public string RedirectUri { get; set; }
}

public class SpotifyApiEndpoints
{
    public string AuthUrl { get; set; }
    public string AuthHeader { get; set; }
}