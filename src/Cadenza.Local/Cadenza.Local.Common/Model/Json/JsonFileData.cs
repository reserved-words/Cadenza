namespace Cadenza.Local.Common.Model.Json;

public class JsonFileData
{
    public JsonAlbum Album { get; set; }
    public JsonArtist AlbumArtist { get; set; }
    public JsonAlbumTrackLink AlbumTrackLink { get; set; }
    public JsonTrack Track { get; set; }
    public JsonArtist TrackArtist { get; set; }
}