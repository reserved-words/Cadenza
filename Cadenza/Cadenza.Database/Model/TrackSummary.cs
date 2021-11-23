using Cadenza.Common;

namespace Cadenza.Database;

public class TrackSummary
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string AlbumTitle { get; set; }
    public string AlbumArtist { get; set; }
    public ReleaseType ReleaseType { get; set; }
    public string Year { get; set; }
    public string Artwork { get; set; }
}

public class TrackDetail
{
    // to do
}