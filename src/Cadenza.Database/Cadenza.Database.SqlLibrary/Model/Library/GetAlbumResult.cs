namespace Cadenza.Database.SqlLibrary.Model.Library;

public class GetAlbumResult
{
    public int Id { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public string Title { get; set; }
    public int ReleaseTypeId { get; set; }
    public string Year { get; set; }
}
