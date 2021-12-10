using Cadenza.Common;

namespace Cadenza.Database;

public class DbTrack
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string ArtistId { get; set; }
    public string AlbumId { get; set; }
    public int DurationSeconds { get; set; }
    public int TrackNo { get; set; }
    public int DiscNo { get; set; }
    public string Year { get; set; }
    public LibrarySource Source { get; set; }
    public string Lyrics { get; set; }
    public ICollection<string> Tags { get; set; } = new List<string>();
}
