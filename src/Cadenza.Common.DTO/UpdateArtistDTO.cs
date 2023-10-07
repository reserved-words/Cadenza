namespace Cadenza.Common.DTO;

public class UpdateArtistDTO
{
    public int Id { get; set; }

    [ItemProperty(ItemProperty.Grouping)]
    public int GroupingId { get; set; }

    [ItemProperty(ItemProperty.Genre)]
    public string Genre { get; set; }
    [ItemProperty(ItemProperty.City)]
    public string City { get; set; }

    [ItemProperty(ItemProperty.State)]
    public string State { get; set; }

    [ItemProperty(ItemProperty.Country)]
    public string Country { get; set; }

    [ItemProperty(ItemProperty.ArtistImage)]
    public string ImageBase64 { get; set; }

    [ItemProperty(ItemProperty.ArtistTags)]
    public ICollection<string> Tags { get; set; }
}
