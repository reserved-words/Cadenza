namespace Cadenza.Database.SqlLibrary.Model.Update;

internal class AddAlbumParameter
{
    public string ArtworkMimeType { get; set; }
    public byte[] ArtworkContent { get; set; }
    public int SourceId { get; set; }
    public int ArtistId { get; set; }
    public string Title { get; set; }
    public int ReleaseTypeId { get; set; }
    public string Year { get; set; }
    public int DiscCount { get; set; }
    public string TagList { get; set; }
}
