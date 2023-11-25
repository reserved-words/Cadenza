using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Search;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Search : ISearch
{
    private readonly ISqlAccess _sql;

    public Search(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Search));
    }

    public async Task<List<GetAlbumsResult>> GetAlbums()
    {
        return await _sql.Query<GetAlbumsResult>(null);
    }

    public async Task<List<GetArtistsResult>> GetArtists()
    {
        return await _sql.Query<GetArtistsResult>(null);
    }

    public async Task<List<GetGenresResult>> GetGenres()
    {
        return await _sql.Query<GetGenresResult>(null);
    }

    public async Task<List<GetGroupingsResult>> GetGroupings()
    {
        return await _sql.Query<GetGroupingsResult>(null);
    }

    public async Task<List<GetTagsResult>> GetTags()
    {
        return await _sql.Query<GetTagsResult>(null);
    }

    public async Task<List<GetTracksResult>> GetTracks()
    {
        return await _sql.Query<GetTracksResult>(null);
    }
}
