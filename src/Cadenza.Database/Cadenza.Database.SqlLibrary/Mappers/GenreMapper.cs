using Cadenza.Common;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class GenreMapper : IGenreMapper
{
    public string MapGenreId(string grouping, string genre)
    {
        return genre.GenreId(grouping);
    }

    public string MapGenreSearchName(string grouping, string genre, bool isUniqueGenre)
    {
        return isUniqueGenre ? genre : $"{genre} ({grouping})";
    }
}
