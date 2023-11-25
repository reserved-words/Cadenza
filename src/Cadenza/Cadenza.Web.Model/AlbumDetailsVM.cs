namespace Cadenza.Web.Model;

public record AlbumDetailsVM : AlbumVM
{
    public IReadOnlyCollection<string> Tags { get; init; }
}