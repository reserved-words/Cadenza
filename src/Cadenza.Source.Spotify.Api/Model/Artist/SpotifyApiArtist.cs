using Cadenza.Source.Spotify.Api.Model.Common;

namespace Cadenza.Source.Spotify.Api.Model.Artist;

public class SpotifyApiArtist
{
    public SpotifyApiExternalUrls external_urls { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string type { get; set; } // e.g. "artist"
    public string uri { get; set; }
}
