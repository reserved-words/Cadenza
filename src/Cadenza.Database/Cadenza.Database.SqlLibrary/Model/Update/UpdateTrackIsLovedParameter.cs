namespace Cadenza.Database.SqlLibrary.Model.Update;

internal class UpdateTrackIsLovedParameter
{
    public string Username { get; set; }
    public int TrackId { get; set; }
    public bool IsLoved { get; set; }
}
