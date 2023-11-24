namespace Cadenza.Database.SqlLibrary.Model.Library;

public class GetAlbumTracksResult
{
    public int TrackId { get; set; }
    public string IdFromSource { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public int DurationSeconds { get; set; }
    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
}
