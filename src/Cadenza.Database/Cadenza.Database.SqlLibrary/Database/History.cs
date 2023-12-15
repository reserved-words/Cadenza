using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Database;

internal class History : IHistory
{
    private readonly ISqlAccess _sql;

    public History(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(History));
    }

    public async Task<List<GetRecentlyAddedAlbumsResult>> GetRecentlyAddedAlbums(int maxItems)
    {
        return await _sql.Query<GetRecentlyAddedAlbumsResult>(new { MaxItems = maxItems });
    }

    public async Task<List<GetRecentlyPlayedAlbumsResult>> GetRecentlyPlayedAlbums(int maxItems)
    {
        return await _sql.Query<GetRecentlyPlayedAlbumsResult>(new { MaxItems = maxItems });
    }

    public async Task<List<GetRecentTagsResult>> GetRecentTags(int maxItems)
    {
        return await _sql.Query<GetRecentTagsResult>(new { MaxItems = maxItems });
    }

    public async Task<IEnumerable<GetRecentTracksResult>> GetRecentTracks(string username, int maxItems)
    {
        return await _sql.Query<GetRecentTracksResult>(new { Username = username, MaxItems = maxItems });
    }

    public async Task<List<GetTopAlbumsResult>> GetTopAlbums(GetTopAlbumsParameter parameter)
    {
        return await _sql.Query<GetTopAlbumsResult>(parameter);
    }

    public async Task<List<GetTopArtistsResult>> GetTopArtists(GetTopArtistsParameter parameter)
    {
        return await _sql.Query<GetTopArtistsResult>(parameter);
    }

    public async Task<List<GetTopTracksResult>> GetTopTracks(GetTopTracksParameter parameter)
    {
        return await _sql.Query<GetTopTracksResult>(parameter);
    }

    public async Task InsertNowPlaying(InsertNowPlayingParameter parameters)
    {
        await _sql.Execute(parameters);
    }

    public async Task InsertScrobble(InsertScrobbleParameter parameters)
    {
        await _sql.Execute(parameters);
    }

    public async Task SyncScrobbles()
    {
        await _sql.Execute(null);
    }
}
