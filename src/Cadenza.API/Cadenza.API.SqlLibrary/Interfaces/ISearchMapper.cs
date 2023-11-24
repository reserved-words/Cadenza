﻿using Cadenza.Database.SqlLibrary.Model.Search;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface ISearchMapper
{
    SearchItemDTO MapAlbum(GetAlbumsResult result);
    SearchItemDTO MapArtist(GetArtistsResult result);
    SearchItemDTO MapGenre(GetGenresResult result);
    SearchItemDTO MapGrouping(GetGroupingsResult result);
    SearchItemDTO MapTag(GetTagsResult result);
    SearchItemDTO MapTrack(GetTracksResult result);
}
