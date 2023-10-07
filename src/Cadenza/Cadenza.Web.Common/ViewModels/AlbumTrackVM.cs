namespace Cadenza.Web.Common.ViewModels;

public class AlbumTrackVM
{
    public AlbumTrackVM()
    {

    }

    public AlbumTrackVM(TrackDetailsVM track, AlbumTrackLinkVM albumTrack)
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
