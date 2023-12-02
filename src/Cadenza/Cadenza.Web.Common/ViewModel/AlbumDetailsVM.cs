namespace Cadenza.Web.Common.ViewModel;

public record AlbumDetailsVM : AlbumVM
{
    public IReadOnlyCollection<string> Tags { get; init; }
}