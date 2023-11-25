namespace Cadenza.Common.DTO;

public class UpdateAlbumTracksDTO
{
    public int AlbumId { get; set; }
    public List<UpdatedAlbumTrackPropertiesDTO> OriginalTracks { get; set; }
    public List<UpdatedAlbumTrackPropertiesDTO> UpdatedTracks { get; set; }
}