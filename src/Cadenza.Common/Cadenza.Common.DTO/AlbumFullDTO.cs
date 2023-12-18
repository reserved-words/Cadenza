namespace Cadenza.Common.DTO;

public class AlbumFullDTO
{
    public AlbumDetailsDTO Album { get; set; }
    public ArtistDTO Artist { get; set; }
    public List<AlbumDiscDTO> Discs { get; set; }
}