﻿using Cadenza.Database.SqlLibrary.Model.Play;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IPlay
{
    Task<GetAlbumResult> GetAlbum(int id);
    Task<GetArtistResult> GetArtist(int id);
    Task<GetGenreResult> GetGenre(string grouping, string genre);
    Task<GetTrackResult> GetTrack(int id);

    Task<List<int>> GetAllTrackIds();
    Task<List<int>> GetAlbumTrackIds(int albumId);
    Task<List<int>> GetArtistTrackIds(int artistId);
    Task<List<int>> GetGenreTrackIds(string genre, string grouping);
    Task<List<int>> GetGroupingTrackIds(string grouping);
    Task<List<int>> GetTagTrackIds(string tag);
}
