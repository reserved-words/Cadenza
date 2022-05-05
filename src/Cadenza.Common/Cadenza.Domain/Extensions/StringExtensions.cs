namespace Cadenza.Domain;

public static class StringExtensions
{
    public static TEnum Parse<TEnum>(this string str, TEnum? defaultValue = null) where TEnum : struct, Enum
    {
        return str.HasMatch(out TEnum match)
            ? match
            : defaultValue.HasValue
            ? defaultValue.Value
            : default(TEnum);
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

    public static string Nullify(this string text)
    {
        return string.IsNullOrWhiteSpace(text)
            ? null
            : text;
    }
}