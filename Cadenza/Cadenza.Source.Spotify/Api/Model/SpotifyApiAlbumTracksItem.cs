﻿namespace Cadenza.Source.Spotify;

public class SpotifyApiAlbumTracksItem
{
    public List<SpotifyApiAlbumArtist> artists { get; set; }
    public int disc_number { get; set; }
    public int duration_ms { get; set; }
    public SpotifyApiExternalUrls external_urls { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public int track_number { get; set; }
    public string type { get; set; } // e.g. "track"
    public string uri { get; set; } // e.g. "spotify:track:3VNWq8rTnQG6fM1eldSpZ0"

}
