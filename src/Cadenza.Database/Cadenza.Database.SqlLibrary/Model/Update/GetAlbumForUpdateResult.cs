namespace Cadenza.Database.SqlLibrary.Model.Update;

internal class GetAlbumForUpdateResult
{
    public int ArtistId { get; set; }
    public string Title { get; set; }
    public int ReleaseTypeId { get; set; }
    public string Year { get; set; }
    public string ArtworkMimeType { get; set; }
    public byte[] ArtworkContent { get; set; }
    public string TagList { get; set; }
}
