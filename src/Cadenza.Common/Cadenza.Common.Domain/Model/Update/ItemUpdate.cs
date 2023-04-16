namespace Cadenza.Common.Domain.Model.Update;

public class ItemUpdate<TInterface> where TInterface : new()
{
    public ItemUpdate()
    {
        OriginalItem = new();
        UpdatedItem = new();
    }

    public ItemUpdate(LibraryItemType type, string id, TInterface originalItem)
    {
        Id = id;
        Type = type;
        OriginalItem = originalItem;
        UpdatedItem = new TInterface();
        CopyValues(originalItem, UpdatedItem);
    }

    public string Id { get; set; }

    public LibraryItemType Type { get; set; }

    public TInterface OriginalItem { get; set; }

    public TInterface UpdatedItem { get; set; }

    public List<PropertyUpdate> Updates { get; set; } = new();

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

    public bool IsUpdated(ItemProperty property, out PropertyUpdate update)
    {
        update = Updates.SingleOrDefault(p => p.Property == property);
        return update != null;
    }

    private static void CopyValues(TInterface sourceItem, TInterface targetItem)
    {
        var properties = typeof(TInterface).GetProperties();

        foreach (var property in properties)
        {
            var originalValue = property.GetValue(sourceItem);

            var updateValue = originalValue is TagList tagList
                ? new TagList(tagList)
                : originalValue;

            property.SetValue(targetItem, updateValue);
        }
    }

    private List<PropertyUpdate> GetUpdates()
    {
        var updates = new List<PropertyUpdate>();

        var properties = typeof(TInterface).GetProperties();

        foreach (var property in properties)
        {
            var itemProperty = property.GetCustomAttributes(false)
                .OfType<ItemPropertyAttribute>()
                .SingleOrDefault();

            if (itemProperty == null)
                continue;

            var originalValue = property.GetValue(OriginalItem)?.ToString();
            var updatedValue = property.GetValue(UpdatedItem)?.ToString();

            if (AreEqual(originalValue, updatedValue))
                continue;

            updates.Add(new PropertyUpdate
            {
                Property = itemProperty.Property,
                OriginalValue = originalValue,
                UpdatedValue = updatedValue
            });
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