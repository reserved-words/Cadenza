
using Cadenza.Domain.Enums;

namespace Cadenza.Core.Model;

public struct ViewItem
{
    public ViewItem(PlayerItemType type, string id, string name)
    {
        Type = type;
        Id = id;
        Name = name;
    }

    public PlayerItemType Type { get; }
    public string Id { get; }
    public string Name { get; }
}
