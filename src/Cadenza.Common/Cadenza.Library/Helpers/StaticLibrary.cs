using Cadenza.Domain;

namespace Cadenza.Library;

public class StaticLibrary
{
    public List<ArtistInfo> Artists { get; set; } = new List<ArtistInfo>();

    public List<TrackInfo> Tracks { get; set; } = new List<TrackInfo>();

    public List<AlbumInfo> Albums { get; set; } = new List<AlbumInfo>();

    public List<AlbumTrackLink> AlbumTrackLinks { get; set; } = new List<AlbumTrackLink>();
}
