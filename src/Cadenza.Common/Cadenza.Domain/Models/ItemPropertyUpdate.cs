using Cadenza.Domain.Enums;

namespace Cadenza.Domain.Models;

public class ItemUpdates
{
    public LibraryItemType Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public List<PropertyUpdate> Updates { get; set; } = new();
}

public class PropertyUpdate
{
    public ItemProperty Property { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}