namespace Cadenza.Web.Common.ViewModel;

public record ArtistDetailsVM
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Grouping { get; init; }
    public string Genre { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ImageBase64 { get; init; }
    public IReadOnlyCollection<string> Tags { get; init; }

    public override string ToString() => Name;
}
