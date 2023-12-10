namespace Cadenza.Web.Common.ViewModel;

public record UpdateAlbumTrackVM
{
    public int TrackId { get; init; }
    public string IdFromSource { get; init; }
    public string ArtistName { get; init; }
    public int DiscNo { get; init; }
    public int DiscTrackCount { get; init; }
    public int TrackNo { get; init; }
    public string Title { get; init; }
}