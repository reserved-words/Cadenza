﻿using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Library : ILibrary
{
    private readonly ISqlAccess _sql;

    public Library(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Library));
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int id)
    {
        return await _sql.Query<string>(new { Id = id });
    }

    public async Task<List<string>> GetTrackSourceIds(LibrarySource source)
    {
        return await _sql.Query<string>(new { SourceId = (int)source });
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int id)
    {
        return await _sql.Query<string>(new { Id = id });
    }

    public async Task<string> GetTrackIdFromSource(int id)
    {
        return await _sql.QuerySingle<string>(new { Id = id });
    }

    public async Task<List<GetTaggedItemsResult>> GetTaggedItems(string tag)
    {
        return await _sql.Query<GetTaggedItemsResult>(new { Tag = tag });
    }

    public async Task<GetFullArtistResult> GetFullArtist(int id)
    {
        return await _sql.QuerySingle<GetFullArtistResult>(new { Id = id });
    }

    public async Task<GetTrackResult> GetTrack(int id)
    {
        return await _sql.QuerySingle<GetTrackResult>(new { Id = id });
    }

    public async Task<GetFullTrackResult> GetFullTrack(int id)
    {
        return await _sql.QuerySingle<GetFullTrackResult>(new { Id = id });
    }

    public async Task<GetFullAlbumResult> GetFullAlbum(int id)
    {
        return await _sql.QuerySingle<GetFullAlbumResult>(new { Id = id });
    }

    public async Task<List<GetAlbumDiscsResult>> GetAlbumDiscs(int id)
    {
        return await _sql.Query<GetAlbumDiscsResult>(new { Id = id });
    }

    public async Task<List<GetAlbumTracksResult>> GetAlbumTracks(int id)
    {
        return await _sql.Query<GetAlbumTracksResult>(new { Id = id });
    }

    public async Task<List<GetArtistAlbumsResult>> GetArtistAlbums(int artistId)
    {
        return await _sql.Query<GetArtistAlbumsResult>(new { ArtistId = artistId });
    }

    public async Task<List<GetAlbumsFeaturingArtistResult>> GetAlbumsFeaturingArtist(int artistId)
    {
        return await _sql.Query<GetAlbumsFeaturingArtistResult>(new { ArtistId = artistId });
    }

    public async Task<List<GetArtistsByGroupingResult>> GetArtistsByGrouping(string grouping)
    {
        return await _sql.Query<GetArtistsByGroupingResult>(new { Grouping = grouping });
    }

    public async Task<List<GetArtistsByGenreResult>> GetArtistsByGenre(string genre, string grouping)
    {
        return await _sql.Query<GetArtistsByGenreResult>(new { Genre = genre, Grouping = grouping });
    }

    public async Task<List<string>> GetGroupings()
    {
        var groupings = await _sql.Query<GetGroupingsResult>(null);
        return groupings.Select(g => g.Grouping).ToList();
    }
}
