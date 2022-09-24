using Cadenza.Domain.Enums;

namespace Cadenza.Domain.Attributes;

public class ItemPropertyAttribute : Attribute
{
    public ItemPropertyAttribute(ItemProperty property)
    {
        Property = property;
    }

    public ItemProperty Property { get; set; }
}
