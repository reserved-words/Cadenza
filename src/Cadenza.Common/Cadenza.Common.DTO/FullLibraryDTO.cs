namespace Cadenza.Common.DTO;

public class FullLibraryDTO
{
    public List<ArtistDetailsDTO> Artists { get; set; }

    public List<TrackDetailsDTO> Tracks { get; set; }

    public List<AlbumDetailsDTO> Albums { get; set; }

    public List<AlbumTrackLinkDTO> AlbumTracks { get; set; }
}
