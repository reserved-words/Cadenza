namespace Cadenza.Common.DTO;

public class UpdateAlbumTracksDTO
{
    public int AlbumId { get; set; }
    public List<AlbumTrackDTO> OriginalTracks { get; set; }
    public List<AlbumTrackDTO> UpdatedTracks { get; set; }
}