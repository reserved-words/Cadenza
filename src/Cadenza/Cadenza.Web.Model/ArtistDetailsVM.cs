namespace Cadenza.Web.Model;

public record ArtistDetailsVM : ArtistVM
{
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ImageBase64 { get; init; }
    public IReadOnlyCollection<string> Tags { get; init; }
}
