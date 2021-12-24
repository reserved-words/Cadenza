namespace Cadenza.Source.Spotify;

public class SpotifyApiArtist
{
    public SpotifyApiExternalUrls external_urls { get; set; }
    public string href { get; set; } // e.g. "https://api.spotify.com/v1/artists/58RMTlPJKbmpmVk1AmRK3h"
    public string id { get; set; } // e.g. "58RMTlPJKbmpmVk1AmRK3h"
    public string name { get; set; }
    public string type { get; set; } // e.g. "artist"
    // public string uri { get; set; } // e.g. "spotify:artist:58RMTlPJKbmpmVk1AmRK3h"
}
