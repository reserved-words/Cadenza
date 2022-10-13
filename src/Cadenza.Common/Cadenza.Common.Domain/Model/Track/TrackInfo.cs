namespace Cadenza.Common.Domain.Model.Track;

public class TrackInfo : Track
{
    //[ItemProperty(ItemProperty.TrackYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.Lyrics)]
    public string Lyrics { get; set; }

    public ICollection<string> Tags { get; set; } = new List<string>();
}
