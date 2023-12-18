namespace Cadenza.Common.DTO;

public class ArtistFullDTO
{
    public ArtistDetailsDTO Artist { get; set; }
    public List<AlbumDTO> Albums { get; set; }
    public List<AlbumDTO> AlbumsFeaturingArtist { get; set; }
}