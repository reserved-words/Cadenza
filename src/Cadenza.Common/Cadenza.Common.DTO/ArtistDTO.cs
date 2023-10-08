namespace Cadenza.Common.DTO;

public class ArtistDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    [ItemProperty(ItemProperty.ArtistGrouping)]
    public GroupingDTO Grouping { get; set; }

    [ItemProperty(ItemProperty.ArtistGenre)]
    public string Genre { get; set; }
}
