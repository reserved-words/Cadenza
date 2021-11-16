﻿namespace Cadenza.Source.Spotify;

internal class SpotifyApiAlbum
{
    public string album_type { get; set; }
    public List<SpotifyApiAlbumArtist> artists { get; set; }
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
    public string uri { get; set; } // e.g. "spotify:album:5m4VYOPoIpkV0XgOiRKkWC"
}