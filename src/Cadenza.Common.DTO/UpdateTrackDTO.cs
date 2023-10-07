namespace Cadenza.Common.DTO;

public class UpdateTrackDTO
{
    public int Id { get; set; }

    [ItemProperty(ItemProperty.TrackTitle)]
    public string Title { get; set; }

    [ItemProperty(ItemProperty.TrackYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.Lyrics)]
    public string Lyrics { get; set; }

    [ItemProperty(ItemProperty.TrackTags)]
    public ICollection<string> Tags { get; set; }
}