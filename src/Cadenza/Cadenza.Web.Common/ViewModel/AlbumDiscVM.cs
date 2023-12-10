namespace Cadenza.Web.Common.ViewModel;

public record AlbumDiscVM
{
    public int DiscNo { get; init; }
    public int TrackCount { get; init; }
    public IReadOnlyCollection<AlbumTrackVM> Tracks { get; init; }
}
