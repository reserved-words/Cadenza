using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.Common.Domain.Model.Track;

public class TrackFull
{
    public TrackInfo Track { get; set; } = new();
    public ArtistInfo Artist { get; set; } = new();
    public AlbumInfo Album { get; set; } = new();
    public ArtistInfo AlbumArtist { get; set; } = new();
    public AlbumTrackLink AlbumTrack { get; set; } = new();
}
