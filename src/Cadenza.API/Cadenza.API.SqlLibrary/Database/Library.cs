using Dapper;
using System.Data;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Library : ILibrary
{
    private ISqlAccess _sql;

    public Library(ISqlAccess sql)
    {
        _sql = sql;
    }public async Task<AlbumData> GetAlbum(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.QuerySingle<AlbumData>("[Library].[GetAlbum]", parameters);
    }

    public async Task<AlbumArtwork> GetAlbumArtwork(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.QuerySingle<AlbumArtwork>("[Library].[GetAlbumArtwork]", parameters);
    }

    public async Task<List<GetAlbumData>> GetAlbums(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetAlbumData>("[Library].[GetAlbums]", parameters);
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.Query<string>("[Library].[GetAlbumTrackSourceIds]", parameters);
    }

    public async Task<List<string>> GetTrackSourceIds(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int)source);
        return await _sql.Query<string>("[Library].[GetTrackSourceIds]", parameters);
    }

    public async Task<ArtistData> GetArtist(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.QuerySingle<ArtistData>("[Library].[GetArtist]", parameters);
    }

    public async Task<ArtistImage> GetArtistImage(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.QuerySingle<ArtistImage>("[Library].[GetArtistImage]", parameters);
    }

    public async Task<List<GetArtistData>> GetArtists()
    {
        return await _sql.Query<GetArtistData>("[Library].[GetArtists]", null);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.Query<string>("[Library].[GetArtistTrackSourceIds]", parameters);
    }

    public async Task<List<GetDiscData>> GetDiscs(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetDiscData>("[Library].[GetDiscs]", parameters);
    }


    public async Task<TrackData> GetTrack(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", trackId);
        return await _sql.QuerySingle<TrackData>("[Library].[GetTrack]", parameters);
    }

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", trackId);
        return await _sql.QuerySingle<string>("[Library].[GetTrackIdFromSource]", parameters);
    }

    public async Task<List<GetTrackData>> GetTracks(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetTrackData>("[Library].[GetTracks]", parameters);

    }
}
