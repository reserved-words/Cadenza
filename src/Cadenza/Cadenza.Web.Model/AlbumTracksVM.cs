namespace Cadenza.Web.Model;

public record AlbumTracksVM
{
    public int AlbumId { get; init; }
    public int DiscCount { get; set; }
    public IReadOnlyCollection<AlbumDiscVM> Discs { get; init; }
}