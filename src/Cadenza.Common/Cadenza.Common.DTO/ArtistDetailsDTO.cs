using Cadenza.Common.Enums;

namespace Cadenza.Common.DTO;

public class ArtistDetailsDTO : ArtistDTO
{
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
