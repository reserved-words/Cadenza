namespace Cadenza.Database.SqlLibrary.Model.Library;

public class GetArtistsByGroupingResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GroupingId { get; set; }
    public string GroupingName { get; set; }
    public string Genre { get; set; }
}
