namespace Cadenza.Common;

public class AlbumInfo : Album
{
    [ItemProperty(ItemProperty.ReleaseYear)]
    public string Year { get; set; }

    public string ImageUrl { get; set; }

    [ItemProperty(ItemProperty.DiscCount)]
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; } = new List<int>();
}
