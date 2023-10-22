namespace Cadenza.Web.Model;

public record AlbumTracksVM
{
    public int AlbumId { get; init; }
    public IReadOnlyCollection<AlbumDiscVM> Discs { get; init; }
}