using Cadenza.Spotify.API.Model.Common;

namespace Cadenza.Spotify.API.Model.Albums;

public class SpotifyApiAlbum
{
    public string album_type { get; set; }
    public List<SpotifyApiArtist> artists { get; set; }
    public SpotifyApiExternalUrls external_urls { get; set; }
    public List<string> genres { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public List<SpotifyApiImage> images { get; set; }
    public string name { get; set; }
    public int popularity { get; set; }
    public string release_date { get; set; } // e.g. "2013-01-01"
    public string release_date_precision { get; set; } // e.g. "day"
    public SpotifyApiAlbumTracks tracks { get; set; }
    public string type { get; set; } // e.g. "album"
    public string uri { get; set; }
}