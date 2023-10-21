namespace Cadenza.Web.Common.Model;

public class EditableAlbumTrack
{
    public int TrackId { get; init; }
    public int ArtistId { get; init; }
    public string ArtistName { get; init; }
    public int DurationSeconds { get; init; }
    public string IdFromSource { get; init; }

    public string Title { get; set; }
    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
}
