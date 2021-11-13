using System.ComponentModel.DataAnnotations;

namespace Cadenza.Common;

public static class EnumExtensionMethods
{
    internal static T GetAttribute<T>(this Enum en) where T : Attribute
    {
        var memInfo = en.GetType().GetMember(en.ToString());

        if (memInfo.IsNullOrEmpty())
            return null;

        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false).OfType<T>();

        return attributes.FirstOrDefault();
    }

    public static string GetDisplayName(this Enum en)
    {
        return en.GetAttribute<DisplayAttribute>()?.Name ?? en.ToString();
    }
}