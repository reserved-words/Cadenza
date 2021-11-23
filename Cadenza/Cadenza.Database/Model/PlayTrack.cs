using Cadenza.Common;

namespace Cadenza.Database;

public class PlayTrack
{
    public string Id { get; set; }
    public LibrarySource Source { get; set; }
    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
}
