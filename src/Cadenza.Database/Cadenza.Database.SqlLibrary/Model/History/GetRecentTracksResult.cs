namespace Cadenza.Database.SqlLibrary.Model.History;

public class GetRecentTracksResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public int AlbumId { get; set; }
    public string AlbumTitle { get; set; }
    public bool IsLoved { get; set; }
    public DateTime ScrobbledAt { get; set; }
    public bool NowPlaying { get; set; }
}