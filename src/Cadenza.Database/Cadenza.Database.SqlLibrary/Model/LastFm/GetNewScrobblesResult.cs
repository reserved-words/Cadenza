namespace Cadenza.Database.SqlLibrary.Model.LastFm;

public class GetNewScrobblesResult
{
    public int Id { get; set; }
    public string SessionKey { get; set; }
    public DateTime ScrobbledAt { get; set; }
    public string Track { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string AlbumArtist { get; set; }
}
