namespace Cadenza.State.Model;

public record class ArtistVM()
{
    public int Id { get; init; }
    public string Name { get; init; }
    public GroupingVM Grouping { get; init; }
    public string Genre { get; init; }

    public override string ToString() => Name;
}
