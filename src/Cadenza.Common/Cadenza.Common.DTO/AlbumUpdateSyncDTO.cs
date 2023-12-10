namespace Cadenza.Common.DTO;

public class AlbumUpdateSyncDTO
{
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public string ReleaseType { get; set; }
    public string Year { get; set; }
    public int DiscCount { get; set; }
    //public string ArtworkMimeType { get; set; }
    //public byte[] ArtworkContent { get; set; }
    public string ArtworkBase64 { get; set; }
    public string TagList { get; set; }
}
