namespace Cadenza.Database.SqlLibrary.Database;

internal class Library : ILibrary
{
    private readonly ISqlAccess _sql;

    public Library(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Library));
    }

    public async Task<AlbumData> GetAlbum(int id)
    {
        return await _sql.QuerySingle<AlbumData>(new { Id = id });
    }

    public async Task<List<GetAlbumData>> GetAlbums(LibrarySource? source)
    {
        return await _sql.Query<GetAlbumData>(new { SourceId = (int?)source });
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int id)
    {
        return await _sql.Query<string>(new { Id = id });
    }

    public async Task<List<string>> GetTrackSourceIds(LibrarySource source)
    {
        return await _sql.Query<string>(new { SourceId = (int)source });
    }

    public async Task<ArtistData> GetArtist(int id)
    {
        return await _sql.QuerySingle<ArtistData>(new { Id = id });
    }

    public async Task<List<GetArtistData>> GetArtists()
    {
        return await _sql.Query<GetArtistData>(null);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int id)
    {
        return await _sql.Query<string>(new { Id = id });
    }

    public async Task<List<GetDiscData>> GetDiscs(LibrarySource? source)
    {
        return await _sql.Query<GetDiscData>(new { SourceId = (int?)source });
    }


    public async Task<TrackData> GetTrack(int id)
    {
        return await _sql.QuerySingle<TrackData>(new { Id = id });
    }

    public async Task<string> GetTrackIdFromSource(int id)
    {
        return await _sql.QuerySingle<string>(new { Id = id });
    }

    public async Task<List<GetTrackData>> GetTracks(LibrarySource? source)
    {
        return await _sql.Query<GetTrackData>(new { SourceId = (int?)source });
    }
}
