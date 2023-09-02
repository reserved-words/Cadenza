namespace Cadenza.Common.Domain.Extensions;

public static class StringExtensions
{
    public static string Nullify(this string text)
    {
        return string.IsNullOrWhiteSpace(text)
            ? null
            : text;
    }
}