namespace Cadenza.API.Core.LastFM;

public class Scrobble
{
    public string SessionKey { get; set; }
    public DateTime Timestamp { get; set; }
    public string Artist { get; set; }
    public string Title { get; set; }
    public string AlbumTitle { get; set; }
    public string AlbumArtist { get; set; }
    public int Duration { get; set; }
}
