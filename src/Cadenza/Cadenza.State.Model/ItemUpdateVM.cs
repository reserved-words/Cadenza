namespace Cadenza.State.Model;

public record ItemUpdateVM<TInterface> where TInterface : new()
{
    public ItemUpdateVM(LibraryItemType type, int id, TInterface originalItem)
    {
        Id = id;
        Type = type;
        OriginalItem = originalItem;
        UpdatedItem = new TInterface();
        CopyValues(originalItem, UpdatedItem);
    }

    public int Id { get; }

    public LibraryItemType Type { get; }

    public TInterface OriginalItem { get; }

    public TInterface UpdatedItem { get; }

    public List<PropertyUpdateVM> Updates { get; } = new();

    public void ApplyUpdates(TInterface item)
    {
        CopyValues(UpdatedItem, item);
    }

    public void ConfirmUpdates()
    {
        // Updates = GetUpdates();
    }

    public bool IsUpdated(ItemProperty property)
    {
        return Updates.Any(p => p.Property == property);
    }

    public bool IsUpdated(ItemProperty property, out PropertyUpdateVM update)
    {
        update = Updates.SingleOrDefault(p => p.Property == property);
        return update != null;
    }

    private static void CopyValues(TInterface sourceItem, TInterface targetItem)
    {
        //var properties = typeof(TInterface).GetProperties();

        //foreach (var property in properties)
        //{
        //    var originalValue = property.GetValue(sourceItem);

        //    var updateValue = originalValue is TagListVM tagList
        //        ? new TagListVM(tagList)
        //        : originalValue;

        //    property.SetValue(targetItem, updateValue);
        //}
    }

    private List<PropertyUpdateVM> GetUpdates()
    {
        var updates = new List<PropertyUpdateVM>();

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

            //updates.Add(new PropertyUpdateVM
            //{
            //    Property = itemProperty.Property,
            //    OriginalValue = originalValue,
            //    UpdatedValue = updatedValue
            //});
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