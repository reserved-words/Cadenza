namespace Cadenza.Common.Domain.Model.Update;

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

    public List<EditedProperty> PropertyUpdates { get; set; } = new();

    public void ApplyUpdates(TInterface item)
    {
        CopyValues(UpdatedItem, item);
    }

    public void ConfirmUpdates()
    {
        PropertyUpdates = GetUpdates();
    }

    public bool IsUpdated(ItemProperty property)
    {
        return PropertyUpdates.Any(p => p.Property == property);
    }

    public bool IsUpdated(ItemProperty property, out EditedProperty update)
    {
        update = PropertyUpdates.SingleOrDefault(p => p.Property == property);
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

    private List<EditedProperty> GetUpdates()
    {
        var updates = new List<EditedProperty>();

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

            updates.Add(new EditedProperty
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