using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Common.Model;

public class FullLibrary
{
    public List<ArtistInfo> Artists { get; set; } = new();

    public List<TrackInfo> Tracks { get; set; } = new();

    public List<AlbumInfo> Albums { get; set; } = new();

    public List<AlbumTrackLink> AlbumTrackLinks { get; set; } = new();
}
