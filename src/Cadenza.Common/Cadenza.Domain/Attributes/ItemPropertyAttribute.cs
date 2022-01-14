namespace Cadenza.Domain;

public class ItemPropertyAttribute : Attribute
{
    public ItemPropertyAttribute(ItemProperty property)
    {
        Property = property;
    }

    public ItemProperty Property { get; set; }
}
