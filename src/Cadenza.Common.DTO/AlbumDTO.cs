namespace Cadenza.Common.DTO;

public class AlbumDTO
{
    public int Id { get; set; }
    public int ArtistId { get; set; }

    public string ArtistName { get; set; }

    [ItemProperty(ItemProperty.AlbumTitle)]
    public string Title { get; set; }

    [ItemProperty(ItemProperty.ReleaseType)]
    public ReleaseType ReleaseType { get; set; }

    [ItemProperty(ItemProperty.ReleaseYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.Artwork)]
    public string ArtworkBase64 { get; set; }
}
