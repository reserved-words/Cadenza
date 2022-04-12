using Cadenza.Source.Spotify.Api.Model.Artist;

namespace Cadenza.Source.Spotify.Api.Model.Playlists;

public class SpotifyApiPlaylistItemTrack
{
    public string id { get; set; }
    public string uri { get; set; }
    public string name { get; set; }
    public List<SpotifyApiArtist> artists { get; set; }
    public int duration_ms { get; set; }
    public int track_number { get; set; }
}