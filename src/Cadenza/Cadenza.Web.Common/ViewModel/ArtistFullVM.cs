namespace Cadenza.Web.Common.ViewModel;

public record ArtistFullVM
{
    public ArtistDetailsVM Artist { get; init; }
    public IReadOnlyCollection<AlbumVM> Albums { get; set; }
    public IReadOnlyCollection<AlbumVM> AlbumsFeaturingArtist { get; set; }
}