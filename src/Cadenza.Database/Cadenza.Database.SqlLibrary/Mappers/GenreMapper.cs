using Cadenza.Database.SqlLibrary.Mappers.Interfaces;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class GenreMapper : IGenreMapper
{
    public string MapGenreId(string grouping, string genre)
    {
        return $"{grouping}|{genre}";
    }

    public string MapGenreSearchName(string grouping, string genre, bool isUniqueGenre)
    {
        return isUniqueGenre ? genre : $"{genre} ({grouping})";
    }
}
