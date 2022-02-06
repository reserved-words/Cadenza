namespace Cadenza.Domain;

public class FullLibrary
{
    public List<ArtistInfo> Artists { get; set; }

    public List<TrackInfo> Tracks { get; set; }

    public List<AlbumInfo> Albums { get; set; }

    public List<AlbumTrackLink> AlbumTrackLinks { get; set; }
}
