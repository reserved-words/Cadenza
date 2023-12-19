namespace Cadenza.Common;

public static class GenreExtensions
{
    public static string GenreId(this string genre, string grouping)
    {
        return $"{grouping}|{genre}";
    }

    public static string GetGenreName(this string id)
    {
        (var _, var genre) = id.SplitGenreId();
        return genre;
    }

    public static (string Grouping, string Genre) SplitGenreId(this string id)
    {
        var parts = id.Split('|');
        return (parts[0], parts[1]);
    }
}
