namespace Cadenza.Web.Common.ViewModel;

public record AlbumTrackVM
{
    public int TrackId { get; init; }
    public string Title { get; init; }
    public int ArtistId { get; init; }
    public string ArtistName { get; init; }
    public int DurationSeconds { get; init; }
    public int DiscNo { get; set; }
    public int TrackNo { get; init; }
    public int DiscTrackCount { get; set; }
    public string IdFromSource { get; init; }
}
