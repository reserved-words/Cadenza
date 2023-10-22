namespace Cadenza.Common.DTO;

public class AlbumTracksDTO
{
    public int AlbumId { get; set; }
    public List<AlbumDiscDTO> Discs { get; set; }
}