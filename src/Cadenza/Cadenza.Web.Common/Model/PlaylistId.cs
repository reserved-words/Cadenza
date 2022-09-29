using Cadenza.Domain.Enums;
using Cadenza.Web.Common.Enums;

namespace Cadenza.Web.Common.Model;

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
