namespace Cadenza.Database.SqlLibrary.Model.LastFm;

public class GetLovedTrackUpdatesResult
{
    public int TrackId { get; set; }
    public int UserId { get; set; }
    public string SessionKey { get; set; }
    public string Track { get; set; }
    public string Artist { get; set; }
    public bool IsLoved { get; set; }
}
