namespace Cadenza.Common.Domain.Model.Album;

public class AlbumInfo : Album
{
    //[ItemProperty(ItemProperty.DiscCount)]
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; } = new List<int>();
}
