using Cadenza.Spotify.API.Model.Common;

namespace Cadenza.Spotify.API.Model.Playlists;

public class SpotifyApiPlaylistOwner
{
    public SpotifyApiExternalUrls external_urls { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public string type { get; set; }
    public string uri { get; set; }
}
