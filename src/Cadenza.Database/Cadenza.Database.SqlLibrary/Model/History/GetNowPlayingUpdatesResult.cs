namespace Cadenza.Database.SqlLibrary.Model.History;

public class GetNowPlayingUpdatesResult
{
    public int UserId { get; set; }
    public string SessionKey { get; set; }
    public DateTime Timestamp { get; set; }
    public int SecondsRemaining { get; set; }
    public string Track { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string AlbumArtist { get; set; }
}