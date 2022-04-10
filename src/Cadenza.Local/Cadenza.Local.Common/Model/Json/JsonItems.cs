namespace Cadenza.Local.Common.Model.Json;

public class JsonItems
{
    public List<JsonArtist> Artists { get; set; }
    public List<JsonAlbum> Albums { get; set; }
    public List<JsonTrack> Tracks { get; set; }
    public List<JsonAlbumTrackLink> AlbumTrackLinks { get; set; }
}