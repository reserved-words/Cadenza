namespace Cadenza.Local.Common.Model;

public class LocalFileData
{
    public LocalAlbum Album { get; set; }
    public LocalArtist AlbumArtist { get; set; }
    public LocalAlbumTrackLink AlbumTrackLink { get; set; }
    public LocalTrack Track { get; set; }
    public LocalArtist TrackArtist { get; set; }
}
