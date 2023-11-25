namespace Cadenza.Common.DTO;

public class UpdatedAlbumTrackPropertiesDTO
{
    public int TrackId { get; set; }

    [ItemProperty(ItemProperty.TrackTitle)]
    public string Title { get; set; }

    [ItemProperty(ItemProperty.TrackDiscNo)]
    public int DiscNo { get; set; }

    [ItemProperty(ItemProperty.TrackNo)]
    public int TrackNo { get; set; }
}
