using Cadenza.Database;

namespace Cadenza.Player;

public class PlaylistDefinition
{
    public PlaylistType Type { get; set; }
    public string Name { get; set; }
    public List<PlayTrack> Tracks { get; set; }
    public PlayTrack First { get; set; }
}
