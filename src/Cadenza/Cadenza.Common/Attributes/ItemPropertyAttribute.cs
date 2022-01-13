namespace Cadenza.Common;

public class ItemPropertyAttribute : Attribute
{
    public ItemPropertyAttribute(ItemProperty property)
    {
        Property = property;
    }

    public ItemProperty Property { get; set; }
}
