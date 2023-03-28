using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;
using Dapper;

namespace Cadenza.API.SqlLibrary.Services;

internal class DataReadService : IDataReadService
{
    private const string GetAlbumProcedure = "[Library].[GetAlbum]";
    private const string GetAlbumsProcedure = "[Library].[GetAlbums]";
    private const string GetArtistProcedure = "[Library].[GetArtist]";
    private const string GetArtistsProcedure = "[Library].[GetArtists]";
    private const string GetDiscsProcedure = "[Library].[GetDiscs]";
    private const string GetTrackProcedure = "[Library].[GetTrack]";
    private const string GetTracksProcedure = "[Library].[GetTracks]";
    private const string GetTrackIdsProcedure = "[Library].[GetTrackIds]";

    private const string IdParameter = "@Id";
    private const string IdFromSourceParameter = "@IdFromSource";
    private const string NameIdParameter = "@NameId";
    private const string SourceIdParameter = "@SourceId";

    private IDataAccess _dbAccess;

    public DataReadService(IDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<AlbumData> GetAlbum(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, albumId);
        return await _dbAccess.QuerySingle<AlbumData>(GetAlbumProcedure, parameters);
    }

    public async Task<List<GetAlbumData>> GetAlbums(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<GetAlbumData>(GetAlbumsProcedure, parameters);
    }

    public async Task<List<string>> GetAllTrackIds(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int)source);
        return await _dbAccess.Query<string>(GetTrackIdsProcedure, parameters);
    }

    public async Task<ArtistData> GetArtist(string nameId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(NameIdParameter, nameId);
        return await _dbAccess.QuerySingle<ArtistData>(GetArtistProcedure, parameters);
    }

    public async Task<List<GetArtistData>> GetArtists()
    {
        return await _dbAccess.Query<GetArtistData>(GetArtistsProcedure, null);
    }

    public async Task<List<GetDiscData>> GetDiscs(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<GetDiscData>(GetDiscsProcedure, parameters);

    }

    public async Task<TrackData> GetTrack(string idFromSource)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdFromSourceParameter, idFromSource);
        return await _dbAccess.QuerySingle<TrackData>(GetTrackProcedure, parameters);
    }

    public async Task<List<GetTrackData>> GetTracks(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<GetTrackData>(GetTracksProcedure, parameters);

    }
}
