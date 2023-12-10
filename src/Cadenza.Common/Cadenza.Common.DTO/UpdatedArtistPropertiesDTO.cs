namespace Cadenza.Common.DTO;

public class UpdatedArtistPropertiesDTO
{
    public int ArtistId { get; set; }

    [ItemProperty(ItemProperty.ArtistGrouping)]
    public string GroupingName { get; set; }

    [ItemProperty(ItemProperty.ArtistGenre)]
    public string Genre { get; set; }
    [ItemProperty(ItemProperty.ArtistCity)]
    public string City { get; set; }

    [ItemProperty(ItemProperty.ArtistState)]
    public string State { get; set; }

    [ItemProperty(ItemProperty.ArtistCountry)]
    public string Country { get; set; }

    [ItemProperty(ItemProperty.ArtistImage)]
    public string ImageBase64 { get; set; }

    [ItemProperty(ItemProperty.ArtistTags)]
    public TagsDTO Tags { get; set; }
}