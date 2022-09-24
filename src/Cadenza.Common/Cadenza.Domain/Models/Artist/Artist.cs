using Cadenza.Domain.Enums;

namespace Cadenza.Domain.Models.Artist;

public class Artist
{
    public string Id { get; set; }

    [ItemProperty(ItemProperty.Artist)]
    public string Name { get; set; }

    [ItemProperty(ItemProperty.Grouping)]
    public Grouping Grouping { get; set; }

    [ItemProperty(ItemProperty.Genre)]
    public string Genre { get; set; }

    public override string ToString() => Name;
}
