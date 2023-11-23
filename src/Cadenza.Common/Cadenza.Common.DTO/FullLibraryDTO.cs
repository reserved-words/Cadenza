namespace Cadenza.Common.DTO;

public class FullLibraryDTO
{
    public List<ArtistDetailsDTO> Artists { get; set; } = new ();

    public List<TrackDetailsDTO> Tracks { get; set; } = new();

    public List<AlbumDetailsDTO> Albums { get; set; } = new();

    public List<AlbumTrackLinkDTO> AlbumTracks { get; set; } = new();
}
