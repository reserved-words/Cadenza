namespace Cadenza.Common.DTO;

public class AlbumTrackDTO
{
    public int TrackId { get; set; }
    [ItemProperty(ItemProperty.TrackTitle)]
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public int DurationSeconds { get; set; }

 //   [ItemProperty(ItemProperty.TrackDiscNo)]
    public int DiscNo { get; set; }
 //   [ItemProperty(ItemProperty.TrackNo)]
    public int TrackNo { get; set; }
    public string IdFromSource { get; set; }
}
