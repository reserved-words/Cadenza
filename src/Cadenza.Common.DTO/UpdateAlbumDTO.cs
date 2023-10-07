namespace Cadenza.Common.DTO;

public class UpdateAlbumDTO
{
    public int Id { get; set; }

    [ItemProperty(ItemProperty.AlbumTitle)]
    public string Title { get; set; }

    [ItemProperty(ItemProperty.ReleaseType)]
    public ReleaseType ReleaseType { get; set; }

    [ItemProperty(ItemProperty.ReleaseYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.Artwork)]
    public string ArtworkBase64 { get; set; }

    [ItemProperty(ItemProperty.AlbumTags)]
    public ICollection<string> Tags { get; set; }
}
