namespace Cadenza.Core;

public struct PlaylistId
{
    public PlaylistId(string id, LibrarySource? source, PlaylistType type, string name) : this()
    {
        Id = id;
        Source = source;
        Type = type;
        Name = name;
    }

    public string Id { get; }
    public LibrarySource? Source { get; }
    public PlaylistType Type { get; }
    public string Name { get; }

}

public class PlaylistDefinition
{
    public PlaylistId Id { get; set; }
    public List<PlayTrack> Tracks { get; set; }
}
