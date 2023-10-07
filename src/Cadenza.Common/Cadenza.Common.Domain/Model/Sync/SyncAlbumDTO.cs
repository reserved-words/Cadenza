namespace Cadenza.Common.Domain.Model.Sync;

public class SyncAlbumDTO
{
    public string ArtistName { get; set; }
    public string Title { get; set; }
    public ReleaseType ReleaseType { get; set; }
    public string Year { get; set; }
    public string ArtworkMimeType { get; set; }
    public byte[] ArtworkContent { get; set; }
    public string TagList { get; set; }
    public List<int> TrackCounts { get; set; }
}
