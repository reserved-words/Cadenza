namespace Cadenza.Common.DTO;

public class ArtistDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    [ItemProperty(ItemProperty.Grouping)]
    public GroupingDTO Grouping { get; set; }

    [ItemProperty(ItemProperty.Genre)]
    public string Genre { get; set; }

    public override string ToString() => Name;
}
