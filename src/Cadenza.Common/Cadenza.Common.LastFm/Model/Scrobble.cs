namespace Cadenza.Common.LastFm.Model;

public class Scrobble
{
    public DateTime Timestamp { get; set; }
    public string Artist { get; set; }
    public string Title { get; set; }
    public string AlbumTitle { get; set; }
    public string AlbumArtist { get; set; }
}
