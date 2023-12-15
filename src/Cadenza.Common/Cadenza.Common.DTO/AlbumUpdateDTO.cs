namespace Cadenza.Common.DTO;

public class AlbumUpdateDTO
{
    public int AlbumId { get; set; }
    public UpdatedAlbumPropertiesDTO UpdatedAlbum { get; set; }
    public List<UpdatedAlbumTrackPropertiesDTO> UpdatedAlbumTracks { get; set; }
    public List<int> RemovedTracks { get; set; }
}
