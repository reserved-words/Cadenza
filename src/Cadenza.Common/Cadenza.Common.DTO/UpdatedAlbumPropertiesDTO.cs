namespace Cadenza.Common.DTO;

public class UpdatedAlbumPropertiesDTO
{
    public int AlbumId { get; set; }

    public string Title { get; set; }

    public ReleaseType ReleaseType { get; set; }

    public string Year { get; set; }

    public string ArtworkBase64 { get; set; }
    public int DiscCount { get; set; }

    public TagsDTO Tags { get; set; }
}
