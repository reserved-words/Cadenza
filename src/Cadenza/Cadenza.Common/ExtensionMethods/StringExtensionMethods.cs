namespace Cadenza.Common;

public static class StringExtensionMethods
{
    public static TEnum Parse<TEnum>(this string str) where TEnum : struct, Enum
    {
        if (str.HasMatch(out TEnum match))
        {
            return match;
        }

        return Default.For<TEnum>();
    }

    private static bool HasMatch<TEnum>(this string str, out TEnum match) where TEnum : struct, Enum
    {
        match = default;

        if (string.IsNullOrWhiteSpace(str))
            return false;

        if (Enum.TryParse(str, out match))
            return true;

        if (str.HasDisplayNameMatch(out match))
            return true;

        return false;
    }

    private static bool HasDisplayNameMatch<TEnum>(this string str, out TEnum match) where TEnum : struct, Enum
    {
        match = default;

        foreach (var value in Enum.GetValues<TEnum>())
        {
            if (value.GetDisplayName() == str)
            {
                match = value;
                return true;
            }   
        }

        return false;
    }
}