namespace Cadenza.Common.Domain.Model.Updates;

public class EditedItem
{
    public LibraryItemType Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public List<EditedProperty> Properties { get; set; } = new();
}
