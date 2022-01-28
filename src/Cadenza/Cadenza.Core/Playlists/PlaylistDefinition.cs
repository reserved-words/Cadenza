namespace Cadenza.Core;

public class PlaylistDefinition
{
    public PlaylistType Type { get; set; }
    public string Name { get; set; }
    public List<BasicTrack> Tracks { get; set; }
    public bool MixedSource { get; set; }
}
