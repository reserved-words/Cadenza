namespace Cadenza.Web.Common.ViewModel;

public record AlbumDetailsVM : AlbumVM
{
    public int DiscCount { get; set; }
    public IReadOnlyCollection<string> Tags { get; init; }
}
