using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Common.Domain.Model;

public class FullLibrary
{
    public List<ArtistInfo> Artists { get; set; } = new();

    public List<TrackInfo> Tracks { get; set; } = new();

    public List<AlbumInfo> Albums { get; set; } = new();

    public List<AlbumTrackLink> AlbumTracks { get; set; } = new();
}
