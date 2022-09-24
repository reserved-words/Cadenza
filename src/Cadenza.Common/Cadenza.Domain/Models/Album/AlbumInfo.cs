using Cadenza.Domain.Enums;

namespace Cadenza.Domain.Models.Album;

public class AlbumInfo : Album
{
    [ItemProperty(ItemProperty.DiscCount)]
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; } = new List<int>();
}
