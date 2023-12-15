namespace Cadenza.Common.DTO;

public class ArtistUpdateDTO
{
    public int ArtistId { get; set; }
    public UpdatedArtistPropertiesDTO UpdatedArtist { get; set; }
    public List<UpdatedArtistReleasePropertiesDTO> UpdatedArtistReleases { get; set; }
}