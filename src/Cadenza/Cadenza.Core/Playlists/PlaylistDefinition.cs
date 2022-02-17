namespace Cadenza.Core;

public class PlaylistDefinition
{
    public PlaylistType Type { get; set; }
    public string Name { get; set; }
    public List<PlayTrack> Tracks { get; set; }
    public bool MixedSource { get; set; }
}
