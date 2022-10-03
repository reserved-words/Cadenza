namespace Cadenza.API.Database.Model;

internal class JsonItems
{
    public List<JsonArtist> Artists { get; set; }
    public List<JsonAlbum> Albums { get; set; }
    public List<JsonTrack> Tracks { get; set; }
    public List<JsonAlbumTrack> AlbumTracks { get; set; }
}