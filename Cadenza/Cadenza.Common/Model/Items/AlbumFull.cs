namespace Cadenza.Common;

public class AlbumFull
{
    public AlbumInfo Album { get; set; }
    public ICollection<AlbumTrack> AlbumTracks { get; set; } = new List<AlbumTrack>();

    public ICollection<Disc> Discs => AlbumTracks
        .GroupBy(t => t.Position.DiscNo)
        .Select(g => new Disc
        {
            DiscNo = g.Key,
            TrackCount = g.Count(),
            Tracks = g.ToList()
        })
        .ToList();
}
