namespace Cadenza.API.SqlLibrary.Model;

internal class ArtistDataBase
{
    public string Name { get; set; }
    public int GroupingId { get; set; }
    public string Genre { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string TagList { get; set; }
}
