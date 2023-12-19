using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Play;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Play : IPlay
{
    private readonly ISqlAccess _sql;

    public Play(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Play));
    }

    public async Task<GetAlbumResult> GetAlbum(int id)
    {
        return await _sql.QuerySingle<GetAlbumResult>(new { Id = id });
    }

    public async Task<List<int>> GetAlbumTrackIds(int id)
    {
        return await _sql.Query<int>(new { Id = id, LogRequest = true });
    }


    public async Task<List<int>> GetAllTrackIds()
    {
        return await _sql.Query<int>(new { LogRequest = true });
    }

    public async Task<GetArtistResult> GetArtist(int id)
    {
        return await _sql.QuerySingle<GetArtistResult>(new { Id = id });
    }

    public async Task<List<int>> GetArtistTrackIds(int id)
    {
        return await _sql.Query<int>(new { Id = id, LogRequest = true });
    }

    public async Task<GetGenreResult> GetGenre(string grouping, string genre)
    {
        return await _sql.QuerySingle<GetGenreResult>(new { Grouping = grouping, Genre = genre });
    }

    public async Task<List<int>> GetGenreTrackIds(string genre, string grouping)
    {
        return await _sql.Query<int>(new { Genre = genre, Grouping = grouping, LogRequest = true });
    }

    public async Task<List<int>> GetGroupingTrackIds(string grouping)
    {
        return await _sql.Query<int>(new { Grouping = grouping, LogRequest = true });
    }

    public async Task<List<int>> GetTagTrackIds(string tag)
    {
        return await _sql.Query<int>(new { Tag = tag, LogRequest = true });
    }

    public async Task<GetTrackResult> GetTrack(int id)
    {
        return await _sql.QuerySingle<GetTrackResult>(new { Id = id });
    }
}
