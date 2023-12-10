namespace Cadenza.Common.DTO;

public class UpdatedTrackPropertiesDTO
{
    public int TrackId { get; set; }

    [ItemProperty(ItemProperty.TrackTitle)]
    public string Title { get; set; }

    [ItemProperty(ItemProperty.TrackYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.TrackLyrics)]
    public string Lyrics { get; set; }

    [ItemProperty(ItemProperty.TrackTags)]
    public TagsDTO Tags { get; set; }
}