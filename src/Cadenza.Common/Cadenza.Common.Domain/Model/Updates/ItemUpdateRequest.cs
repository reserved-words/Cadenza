namespace Cadenza.Common.Domain.Model.Updates;

public class ItemUpdateRequest
{
    public LibraryItemType Type { get; set; }
    public string Id { get; set; }
    public List<PropertyUpdate> Updates { get; set; } = new();
}
