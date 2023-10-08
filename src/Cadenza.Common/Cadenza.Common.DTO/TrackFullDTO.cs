namespace Cadenza.Common.DTO;

public class TrackFullDTO
{
    public TrackDetailsDTO Track { get; set; }
    public ArtistDetailsDTO Artist { get; set; }
    public AlbumDetailsDTO Album { get; set; }
    public ArtistDetailsDTO AlbumArtist { get; set; }
    public AlbumTrackLinkDTO AlbumTrack { get; set; }
}
