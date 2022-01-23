namespace Cadenza.Core;

public class PlaylistDefinition
{
    public PlaylistType Type { get; set; }
    public string Name { get; set; }
    public IEnumerable<BasicTrack> Tracks { get; set; }
}
