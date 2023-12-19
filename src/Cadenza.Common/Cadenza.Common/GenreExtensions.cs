namespace Cadenza.Common;

public static class GenreExtensions
{
    public static string GetGenreDisplayName(this string id, bool includeGrouping = false)
    {
        (var grouping, var genre) = id.SplitGenreId();
        return includeGrouping
            ? $"{grouping} - {genre}"
            : genre;
    }

    public static (string Grouping, string Genre) SplitGenreId(this string id)
    {
        var parts = id.Split('|');
        return (parts[0], parts[1]);
    }
}
