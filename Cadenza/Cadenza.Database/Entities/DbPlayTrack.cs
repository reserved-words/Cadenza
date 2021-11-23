using Cadenza.Common;

namespace Cadenza.Database;

public class DbPlayTrack
{
    public string Id { get; set; }
    public LibrarySource Source { get; set; }
    public string ArtistId { get; set; }
    public string AlbumId { get; set; }
    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
}