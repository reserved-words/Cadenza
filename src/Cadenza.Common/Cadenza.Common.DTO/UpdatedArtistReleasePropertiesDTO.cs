namespace Cadenza.Common.DTO;

public class UpdatedArtistReleasePropertiesDTO
{
    public int AlbumId { get; set; }

    public string Title { get; set; }

    public ReleaseType ReleaseType { get; set; }

    public string Year { get; set; }
}