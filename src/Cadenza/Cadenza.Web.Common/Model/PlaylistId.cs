namespace Cadenza.Web.Common.Model;

public struct PlaylistId
{
    public PlaylistId(string id, PlaylistType type, string name) : this()
    {
        Id = id;
        Type = type;
        Name = name;
    }

    public string Id { get; }
    public PlaylistType Type { get; }
    public string Name { get; }

}
