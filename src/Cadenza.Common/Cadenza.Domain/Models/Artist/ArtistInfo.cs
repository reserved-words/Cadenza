using Cadenza.Domain.Enums;

namespace Cadenza.Domain.Models.Artist;

public class ArtistInfo : Artist
{
    [ItemProperty(ItemProperty.City)]
    public string City { get; set; }

    [ItemProperty(ItemProperty.State)]
    public string State { get; set; }

    [ItemProperty(ItemProperty.Country)]
    public string Country { get; set; }
}
