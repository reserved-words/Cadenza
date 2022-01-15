using Cadenza.Domain;
using System.Reflection;

namespace Cadenza.Common;

public class ItemUpdate<TInterface> where TInterface : new()
{
    public ItemUpdate()
    {
        Item = new();
    }

    public ItemUpdate(ItemType itemType, string id, TInterface model)
    {
        ItemType = itemType;
        Id = id;
        Description = model.ToString();
        Item = model;

        foreach (var kvp in OriginalValues)
        {
            UpdatedValues[kvp.Key] = kvp.Value;
        }
    }

    public ItemType ItemType { get; set; }
    public string Id { get; set; }
    public string Description { get; set; }

    private TInterface _item;
    public TInterface Item
    {
        get { return _item; }
        set
        {
            _item = value;
            var props = typeof(TInterface).GetProperties();
            foreach (var prop in props)
            {
                var propertyName = GetPropertyName(prop);

                if (!propertyName.HasValue)
                    continue;

                OriginalValues[propertyName.Value] = prop.GetValue(value)?.ToString();
            }
        }
    }

    public Dictionary<ItemProperty, string> OriginalValues { get; set; } = new();
    public Dictionary<ItemProperty, string> UpdatedValues { get; set; } = new();

    public bool IsUpdated => Updates.Any();
    public List<ItemPropertyUpdate> Updates => GetUpdates();

    private List<ItemPropertyUpdate> GetUpdates()
    {
        return UpdatedValues
            .Join(OriginalValues,
                u => u.Key,
                o => o.Key,
                (u, o) => new ItemPropertyUpdate
                {
                    ItemType = ItemType,
                    Id = Id,
                    Property = u.Key,
                    Item = Description,
                    OriginalValue = o.Value,
                    UpdatedValue = u.Value
                })
            .Where(p => !AreEqual(p.OriginalValue, p.UpdatedValue))
            .ToList();
    }

    protected string GetUpdated(ItemProperty property)
    {
        return UpdatedValues.ValueOrDefault(property);
    }

    protected void SetUpdated(ItemProperty property, string value)
    {
        UpdatedValues[property] = value;
    }

    private bool AreEqual(string originalValue, string updatedValue)
    {
        if (originalValue == null && updatedValue == null)
            return true;

        if (originalValue == null || updatedValue == null)
            return false;

        return originalValue == updatedValue;
    }
    public static ItemProperty? GetPropertyName(PropertyInfo propertyInfo)
    {
        var attr = propertyInfo.GetCustomAttribute<ItemPropertyAttribute>();
        return attr?.Property;
    }
}