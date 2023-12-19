namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface IGenreMapper
{
    string MapGenreId(string grouping, string genre);
    string MapGenreSearchName(string grouping, string genre, bool isUniqueGenre);
}