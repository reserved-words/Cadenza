
namespace Cadenza.Core.Model;

public struct ViewItem
{
    public ViewItem(PlayerItemType type, string id, string name, LibrarySource? source)
    {
        Type = type;
        Id = id;
        Name = name;
        Source = source;
    }

    public PlayerItemType Type { get; }
    public string Id { get; }
    public string Name { get; }
    public LibrarySource? Source { get; }
}
