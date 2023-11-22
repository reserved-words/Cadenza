namespace Cadenza.Database.SqlLibrary.Model;

internal class TrackDataBase
{
    public string IdFromSource { get; set; }
    public int ArtistId { get; set; }
    public int DiscId { get; set; }
    public int TrackNo { get; set; }
    public string Title { get; set; }
    public int DurationSeconds { get; set; }
    public string Year { get; set; }
    public string Lyrics { get; set; }
    public string TagList { get; set; }
}
