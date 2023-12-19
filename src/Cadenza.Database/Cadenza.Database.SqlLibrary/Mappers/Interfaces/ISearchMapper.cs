using Cadenza.Database.SqlLibrary.Model.Search;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface ISearchMapper
{
    SearchItemDTO MapAlbum(GetAlbumsResult result);
    SearchItemDTO MapArtist(GetArtistsResult result);
    SearchItemDTO MapGenre(GetGenresResult result, bool isUniqueGenre);
    SearchItemDTO MapGrouping(GetGroupingsResult result);
    SearchItemDTO MapTag(GetTagsResult result);
    SearchItemDTO MapTrack(GetTracksResult result);
}
