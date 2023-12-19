namespace Cadenza.Web.Common.ViewModel;

public record AlbumFullVM
{
    public AlbumDetailsVM Album { get; init; }
    public ArtistVM Artist { get; init; }
    public IReadOnlyCollection<AlbumDiscVM> Discs { get; init; }

    public int Id => Album.Id;
    public string Title => Album.Title;
}