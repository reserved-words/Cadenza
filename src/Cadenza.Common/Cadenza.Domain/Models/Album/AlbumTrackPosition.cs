namespace Cadenza.Domain.Models.Album;

public class AlbumTrackPosition
{
    public AlbumTrackPosition() { }

    public AlbumTrackPosition(int discNo, int trackNo)
    {
        DiscNo = discNo;
        TrackNo = trackNo;
    }

    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
}
