namespace Cadenza.Database.SqlLibrary.Model.History;

internal class GetTopTracksResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public int Plays { get; set; }
}
