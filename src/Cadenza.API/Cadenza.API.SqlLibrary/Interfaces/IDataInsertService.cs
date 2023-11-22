﻿using Cadenza.Database.SqlLibrary.Model;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IDataInsertService
{
    Task<int> AddArtist(NewArtistData data);
    Task<int> AddAlbum(NewAlbumData data);
    Task<int> AddDisc(NewDiscData data);
    Task<int> AddTrack(NewTrackData data);
    Task AddAlbumUpdate(NewAlbumUpdateData data);
    Task AddArtistUpdate(NewArtistUpdateData data);
    Task AddTrackRemoval(int trackId);
    Task AddTrackUpdate(NewTrackUpdateData data);
    Task LogLibraryPlay();
    Task LogArtistPlay(int artistId);
    Task LogAlbumPlay(int albumId);
    Task LogTrackPlay(int trackId);
    Task LogGroupingPlay(int groupingId);
    Task LogGenrePlay(string genre);
    Task LogTagPlay(string tag);
}
