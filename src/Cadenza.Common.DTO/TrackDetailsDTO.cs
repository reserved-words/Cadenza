namespace Cadenza.Common.DTO;

public class TrackDetailsDTO : TrackDTO
{
    [ItemProperty(ItemProperty.TrackYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.Lyrics)]
    public string Lyrics { get; set; }

    [ItemProperty(ItemProperty.TrackTags)]
    public TagsDTO Tags { get; set; }
}
