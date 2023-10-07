namespace Cadenza.Common.DTO;

public class ItemUpdateDTO<TInterface> where TInterface : new()
{
    public ItemUpdateDTO()
    {
        OriginalItem = new();
        UpdatedItem = new();
    }

    public ItemUpdateDTO(LibraryItemType type, int id, TInterface originalItem)
    {
        Id = id;
        Type = type;
        OriginalItem = originalItem;
        UpdatedItem = new TInterface();
        CopyValues(originalItem, UpdatedItem);
    }

    public int Id { get; set; }

    public LibraryItemType Type { get; set; }

    public TInterface OriginalItem { get; set; }

    public TInterface UpdatedItem { get; set; }

    public List<PropertyUpdateDTO> Updates { get; set; } = new();

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

    public bool IsUpdated(ItemProperty property, out PropertyUpdateDTO update)
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

            var updateValue = originalValue is TagListDTO tagList
                ? new TagListDTO(tagList)
                : originalValue;

            property.SetValue(targetItem, updateValue);
        }
    }

    private List<PropertyUpdateDTO> GetUpdates()
    {
        var updates = new List<PropertyUpdateDTO>();

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

            updates.Add(new PropertyUpdateDTO
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