namespace Cadenza.Source.Spotify.Model;

public class SpotifyArtistSearchResult
{
    public SpotifyArtist Artist { get; set; }
    public List<SpotifyAlbum> Albums { get; set; }
    public List<SpotifyPlaylist> Playlists { get; set; }
}
