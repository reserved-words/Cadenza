namespace Cadenza.Common.DTO;

public class UpdatedAlbumPropertiesDTO
{
    public int AlbumId { get; set; }

    [ItemProperty(ItemProperty.AlbumTitle)]
    public string Title { get; set; }

    [ItemProperty(ItemProperty.AlbumReleaseType)]
    public ReleaseType ReleaseType { get; set; }

    [ItemProperty(ItemProperty.AlbumReleaseYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.AlbumArtwork)]
    public string ArtworkBase64 { get; set; }
    [ItemProperty(ItemProperty.AlbumDiscCount)]
    public int DiscCount { get; set; }

    [ItemProperty(ItemProperty.AlbumTags)]
    public TagsDTO Tags { get; set; }
}
