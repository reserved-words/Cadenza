namespace Cadenza.Player;

public class PlaylistDefinition
{
    public PlaylistType Type { get; set; }
    public string Name { get; set; }
    public SplitList<PlaylistTrackViewModel> Sections { get; set; }
    public PlaylistTrackViewModel First { get; set; }
}
