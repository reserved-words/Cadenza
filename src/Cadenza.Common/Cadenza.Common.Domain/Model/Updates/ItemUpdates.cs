using Cadenza.Common.Domain.Enums;

namespace Cadenza.Common.Domain.Model.Updates;

public class ItemUpdates
{
    public LibraryItemType Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public List<PropertyUpdate> Updates { get; set; } = new();
}
