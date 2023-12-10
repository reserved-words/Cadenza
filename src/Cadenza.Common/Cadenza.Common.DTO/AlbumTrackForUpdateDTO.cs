namespace Cadenza.Common.DTO;

public class AlbumTrackForUpdateDTO
{
    public int TrackId { get; set; }
    public string ArtistName { get; set; }
    public string IdFromSource { get; set; }
    public string Title { get; set; }
    public int DiscNo { get; set; }
    public int DiscTrackCount { get; set; }
    public int TrackNo { get; set; }
}