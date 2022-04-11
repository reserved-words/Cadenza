using Cadenza.Source.Spotify.Api.Model.Common;

namespace Cadenza.Source.Spotify.Api.Model.Playlists;

public class SpotifyApiPlaylistOwner
{
    public string display_name { get; set; }
    public SpotifyApiExternalUrls external_urls { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public string type { get; set; }
    public string uri { get; set; }
}
