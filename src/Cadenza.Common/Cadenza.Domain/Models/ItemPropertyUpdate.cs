namespace Cadenza.Domain;

public class ItemPropertyUpdate
{
    public LibraryItemType ItemType { get; set; }
    public string Id { get; set; }
    public string Item { get; set; }
    public ItemProperty Property { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || obj is not ItemPropertyUpdate update)
            return false;

        return update.ItemType == ItemType
            && update.Id == Id
            && update.Property == Property;
    }

    public override int GetHashCode()
    {
        return ItemType.GetHashCode()
            ^ Id.GetHashCode()
            ^ Property.GetHashCode();
    }
}
