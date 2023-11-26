namespace Cadenza.Database.SqlLibrary.Model.History;

public class InsertScrobbleParameter
{
    public int TrackId { get; set; }
    public string Username { get; set; }
    public DateTime ScrobbledAt { get; set; }
}