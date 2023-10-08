namespace Cadenza.Web.Model;

public record AlbumVM
{
    public int Id { get; init; }
    public int ArtistId { get; init; }

    public string ArtistName { get; init; }
    public string Title { get; init; }
    public ReleaseType ReleaseType { get; init; }
    public string Year { get; init; }
    public string ArtworkBase64 { get; init; }

    public override string ToString() => $"{ArtistName} - {Title}";
}
