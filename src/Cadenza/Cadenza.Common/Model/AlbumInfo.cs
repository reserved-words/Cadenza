namespace Cadenza.Common;

public class AlbumInfo
{
    public LibrarySource Source { get; set; }
    public string Id { get; set; }
    public string ArtistId { get; set; }

    public string ArtistName { get; set; }
    [ItemProperty(ItemProperty.AlbumTitle)]
    public string Title { get; set; }

    [ItemProperty(ItemProperty.ReleaseType)]
    public ReleaseType ReleaseType { get; set; }

    public override string ToString() => $"{ArtistName} - {Title}";

    [ItemProperty(ItemProperty.ReleaseYear)]
    public string Year { get; set; }

    public string ArtworkUrl { get; set; }

    [ItemProperty(ItemProperty.DiscCount)]
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; } = new List<int>();
}
