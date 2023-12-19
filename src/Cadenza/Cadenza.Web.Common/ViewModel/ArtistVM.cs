namespace Cadenza.Web.Common.ViewModel;

public record class ArtistVM()
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Grouping { get; init; }
    public string Genre { get; init; }

    public override string ToString() => Name;
}
