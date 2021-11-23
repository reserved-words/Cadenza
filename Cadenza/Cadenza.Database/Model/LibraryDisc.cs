namespace Cadenza.Database;

public class LibraryDisc
{

    public int DiscNo { get; set; }
    public int TrackCount { get; set; }
    public ICollection<LibraryTrack> Tracks { get; set; }
}
