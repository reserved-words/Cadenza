namespace Cadenza.Common.DTO;

public class ArtistDetailsDTO : ArtistDTO
{
    [ItemProperty(ItemProperty.City)]
    public string City { get; set; }

    [ItemProperty(ItemProperty.State)]
    public string State { get; set; }

    [ItemProperty(ItemProperty.Country)]
    public string Country { get; set; }

    [ItemProperty(ItemProperty.ArtistImage)]
    public string ImageBase64 { get; set; }

    [ItemProperty(ItemProperty.ArtistTags)]
    public TagsDTO Tags { get; set; }
}
