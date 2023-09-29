namespace Cadenza.Common.Domain.Model.Library;

public class TrackDetails : Track
{
    [ItemProperty(ItemProperty.TrackYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.Lyrics)]
    public string Lyrics { get; set; }

    [ItemProperty(ItemProperty.TrackTags)]
    public TagList Tags { get; set; } = new TagList();
}
