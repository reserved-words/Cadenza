namespace Cadenza.Database.SqlLibrary.Model.Library;

public class GetFullAlbumResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ReleaseTypeId { get; set; }
    public string Year { get; set; }
    public int DiscCount { get; set; }
    public string TagList { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public int ArtistGroupingId { get; set; }
    public string ArtistGroupingName { get; set; }
    public string ArtistGenre { get; set; }
}