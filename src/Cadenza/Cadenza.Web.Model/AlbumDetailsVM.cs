namespace Cadenza.Web.Model;

public record AlbumDetailsVM : AlbumVM
{
    public IReadOnlyCollection<string> Tags { get; init; }

    public override string ToString() => base.ToString();
}