namespace Cadenza.API.SqlLibrary.Model;

internal class AlbumData
{
    public int SourceId { get; set; }
    public int ArtistId { get; set; }
    public string Title { get; set; }
    public int ReleaseTypeId { get; set; }
    public string Year { get; set; }
    public int DiscCount { get; set; }
    public string ArtworkUrl { get; set; }
    public string TagList { get; set; }
}
