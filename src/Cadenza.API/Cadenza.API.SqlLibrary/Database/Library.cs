using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Library : ILibrary
{
    private readonly ISqlAccess _sql;

    public Library(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Library));
    }

    public async Task<List<GetAlbumsResult>> GetAlbums(LibrarySource? source)
    {
        return await _sql.Query<GetAlbumsResult>(new { SourceId = (int?)source });
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int id)
    {
        return await _sql.Query<string>(new { Id = id });
    }

    public async Task<List<string>> GetTrackSourceIds(LibrarySource source)
    {
        return await _sql.Query<string>(new { SourceId = (int)source });
    }

    public async Task<List<GetArtistsResult>> GetArtists()
    {
        return await _sql.Query<GetArtistsResult>(null);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int id)
    {
        return await _sql.Query<string>(new { Id = id });
    }

    public async Task<List<GetDiscsResult>> GetDiscs(LibrarySource? source)
    {
        return await _sql.Query<GetDiscsResult>(new { SourceId = (int?)source });
    }

    public async Task<string> GetTrackIdFromSource(int id)
    {
        return await _sql.QuerySingle<string>(new { Id = id });
    }

    public async Task<List<GetTracksResult>> GetTracks(LibrarySource? source)
    {
        return await _sql.Query<GetTracksResult>(new { SourceId = (int?)source });
    }

    public async Task<List<GetTaggedItemsResult>> GetTaggedItems(string tag)
    {
        return await _sql.Query<GetTaggedItemsResult>(new { Tag = tag });
    }

    public async Task<GetArtistResult> GetArtist(int id)
    {
        return await _sql.QuerySingle<GetArtistResult>(new { Id = id });
    }
}
