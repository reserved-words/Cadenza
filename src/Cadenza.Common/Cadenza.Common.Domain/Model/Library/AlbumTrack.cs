namespace Cadenza.Common.Domain.Model.Library;

public class AlbumTrack
{
    public AlbumTrack()
    {

    }

    public AlbumTrack(TrackInfo track, AlbumTrackLink albumTrack)
    {
        TrackId = track.Id;
        Title = track.Title;
        ArtistId = track.ArtistId;
        ArtistName = track.ArtistName;
        DurationSeconds = track.DurationSeconds;
        DiscNo = albumTrack.DiscNo;
        TrackNo = albumTrack.TrackNo;
    }

    public int TrackId { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public int DurationSeconds { get; set; }
    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
}
