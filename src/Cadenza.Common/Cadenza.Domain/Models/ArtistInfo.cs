namespace Cadenza.Domain;

public class ArtistInfo : Artist
{
    [ItemProperty(ItemProperty.City)]
    public string City { get; set; }

    [ItemProperty(ItemProperty.State)]
    public string State { get; set; }

    [ItemProperty(ItemProperty.Country)]
    public string Country { get; set; }

    public ICollection<Link> Links { get; set; } = new List<Link>();
}
