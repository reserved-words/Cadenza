namespace Cadenza.Web.Model;

public record AlbumTrackVM
{
    public int TrackId { get; init; }
    public string Title { get; init; }
    public int ArtistId { get; init; }
    public string ArtistName { get; init; }
    public int DurationSeconds { get; init; }
    public int DiscNo { get; init; }
    public int TrackNo { get; init; }
}
