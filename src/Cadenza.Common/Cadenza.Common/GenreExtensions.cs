namespace Cadenza.Common;

public static class GenreExtensions
{
    public static string GetGenreName(this string id)
    {
        (var grouping, var genre) = id.SplitGenreId();
        return genre;
    }

    public static string GetFullGenreName(this string id)
    {
        (var grouping, var genre) = id.SplitGenreId();
        return $"{grouping} - {genre}";
    }

    public static (string Grouping, string Genre) SplitGenreId(this string id)
    {
        var parts = id.Split('|');
        return (parts[0], parts[1]);
    }

    public static string GenreId(this (string Grouping, string Genre) item)
    {
        return $"{item.Grouping}|{item.Genre}";
    }
}
