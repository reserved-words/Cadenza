using System.Reflection;

namespace Cadenza.Common;

public static class PropertyExtensionsMethods
{
    public static ItemProperty? GetPropertyName(this PropertyInfo propertyInfo)
    {
        var attr = propertyInfo.GetCustomAttribute<ItemPropertyAttribute>();
        return attr?.Property;
    }
}