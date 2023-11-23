using Dapper;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Library : ILibrary
{
    private readonly ISqlAccess _sql;

    public Library(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Library));
    }

    public async Task<AlbumData> GetAlbum(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.QuerySingle<AlbumData>(parameters);
    }

    public async Task<AlbumArtwork> GetAlbumArtwork(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.QuerySingle<AlbumArtwork>(parameters);
    }

    public async Task<List<GetAlbumData>> GetAlbums(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetAlbumData>(parameters);
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.Query<string>(parameters);
    }

    public async Task<List<string>> GetTrackSourceIds(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int)source);
        return await _sql.Query<string>(parameters);
    }

    public async Task<ArtistData> GetArtist(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.QuerySingle<ArtistData>(parameters);
    }

    public async Task<ArtistImage> GetArtistImage(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.QuerySingle<ArtistImage>(parameters);
    }

    public async Task<List<GetArtistData>> GetArtists()
    {
        return await _sql.Query<GetArtistData>(null);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.Query<string>(parameters);
    }

    public async Task<List<GetDiscData>> GetDiscs(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetDiscData>(parameters);
    }


    public async Task<TrackData> GetTrack(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", trackId);
        return await _sql.QuerySingle<TrackData>(parameters);
    }

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", trackId);
        return await _sql.QuerySingle<string>(parameters);
    }

    public async Task<List<GetTrackData>> GetTracks(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetTrackData>(parameters);

    }
}
