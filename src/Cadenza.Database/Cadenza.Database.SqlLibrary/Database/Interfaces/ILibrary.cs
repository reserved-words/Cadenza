﻿using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface ILibrary
{
    Task<List<string>> GetTrackSourceIds(LibrarySource source);

    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<string> GetTrackIdFromSource(int trackId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
    Task<List<GetTaggedItemsResult>> GetTaggedItems(string tag);

    Task<GetFullArtistResult> GetFullArtist(int id);
    Task<GetTrackResult> GetTrack(int id);
    Task<GetFullTrackResult> GetFullTrack(int id);
    Task<GetFullAlbumResult> GetFullAlbum(int id);

    Task<List<GetAlbumDiscsResult>> GetAlbumDiscs(int id);
    Task<List<GetAlbumTracksResult>> GetAlbumTracks(int id);

    Task<List<GetArtistAlbumsResult>> GetArtistAlbums(int artistId);
    Task<List<GetAlbumsFeaturingArtistResult>> GetAlbumsFeaturingArtist(int artistId);

    Task<List<GetArtistsByGroupingResult>> GetArtistsByGrouping(string grouping);
    Task<List<GetArtistsByGenreResult>> GetArtistsByGenre(string genre, string grouping);
    Task<List<string>> GetGroupings();
}
