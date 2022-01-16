namespace Cadenza.Local;

public class JsonAlbum
{
    public string Id { get; set; }
    public string ArtistId { get; set; }
    public string Title { get; set; }
    public string ReleaseType { get; set; }
    public string Year { get; set; }
    public List<int> TrackCounts { get; set; } = new List<int>();
}
