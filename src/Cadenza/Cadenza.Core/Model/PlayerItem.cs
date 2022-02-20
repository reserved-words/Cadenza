
namespace Cadenza.Core.Model;

public struct PlayerItem
{
    public PlayerItem(SearchableItemType type, string id, string name, LibrarySource? source)
    {
        Type = type;
        Id = id;
        Name = name;
        Source = source;
    }

    public SearchableItemType Type { get; }
    public string Id { get; }
    public string Name { get; }
    public LibrarySource? Source { get; }
}
