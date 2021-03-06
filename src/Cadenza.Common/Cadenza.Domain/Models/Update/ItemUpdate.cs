namespace Cadenza.Domain;

public class ItemUpdate<TInterface> where TInterface : new()
{
    public ItemUpdate()
    {
        OriginalItem = new();
        UpdatedItem = new();
    }

    public ItemUpdate(LibraryItemType type, string id, string name, TInterface originalItem)
    {
        Id = id;
        Name = name;
        Type = type;
        OriginalItem = originalItem;
        UpdatedItem = new TInterface();
        CopyValues(originalItem, UpdatedItem);
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public LibraryItemType Type { get; set; }

    public TInterface OriginalItem { get; set; }

    public TInterface UpdatedItem { get; set; }

    public List<ItemPropertyUpdate> Updates { get; set; } = new();

    public void ApplyUpdates(TInterface item)
    {
        CopyValues(UpdatedItem, item);
    }

    public void ConfirmUpdates()
    {
        Updates = GetUpdates();
    }

    public bool IsUpdated(ItemProperty property)
    {
        return Updates.Any(p => p.Property == property);
    }

    public bool IsUpdated(ItemProperty property, out ItemPropertyUpdate update)
    {
        update = Updates.SingleOrDefault(p => p.Property == property);
        return update != null;
    }

    private static void CopyValues(TInterface sourceItem, TInterface targetItem)
    {
        var properties = typeof(TInterface).GetProperties();

        foreach (var property in properties)
        {
            property.SetValue(targetItem, property.GetValue(sourceItem));
        }
    }

    private List<ItemPropertyUpdate> GetUpdates()
    {
        var updates = new List<ItemPropertyUpdate>();

        var properties = typeof(TInterface).GetProperties();

        foreach (var property in properties)
        {
            var originalValue = property.GetValue(OriginalItem)?.ToString();
            var updatedValue = property.GetValue(UpdatedItem)?.ToString();
            if (!AreEqual(originalValue, updatedValue))
            {
                var itemProperty = property.GetCustomAttributes(false)
                    .OfType<ItemPropertyAttribute>()
                    .Single()
                    .Property;

                updates.Add(new ItemPropertyUpdate
                {
                    ItemType = Type,
                    Id = Id,
                    Property = itemProperty,
                    Item = Name,
                    OriginalValue = originalValue,
                    UpdatedValue = updatedValue
                });
            }
        }

        return updates;
    }

    private static bool AreEqual(string originalValue, string updatedValue)
    {
        if (originalValue == null && updatedValue == null)
            return true;

        if (originalValue == null || updatedValue == null)
            return false;

        return originalValue == updatedValue;
    }
}