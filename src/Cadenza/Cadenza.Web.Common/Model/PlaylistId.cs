namespace Cadenza.Web.Common.Model;

public class PlaylistId
{
    public PlaylistId(string id, PlaylistType type, string name)
    {
        Id = id;
        Type = type;
        Name = name;
    }

    public string Id { get; }
    public PlaylistType Type { get; }
    public string Name { get; }

}
