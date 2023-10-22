namespace Cadenza.Common.DTO;

public class AlbumDiscDTO
{
    public int DiscNo { get; set; }
    public int TrackCount { get; set; }
    public List<AlbumTrackDTO> Tracks { get; set; }
}
