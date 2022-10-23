namespace Cadenza.Common.Domain.Model.Album;

public class AlbumInfo : Album
{
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; } = new List<int>();

    [ItemProperty(ItemProperty.AlbumTags)]
    public TagList Tags { get; set; } = new TagList();
}
