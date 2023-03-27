using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;
using Dapper;

namespace Cadenza.API.SqlLibrary.Services;

internal class DataReadService : IDataReadService
{
    private const string GetArtistsProcedure = "[Library].[GetArtists]";
    private const string GetAlbumsProcedure = "[Library].[GetAlbums]";
    private const string GetDiscsProcedure = "[Library].[GetDiscs]";
    private const string GetTracksProcedure = "[Library].[GetTracks]";
    private const string GetTrackIdsProcedure = "[Library].[GetTrackIds]";
    private const string SourceIdParameter = "@SourceId";

    private IDataAccess _dbAccess;

    public DataReadService(IDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
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

    public async Task<List<GetTrackData>> GetTracks(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<GetTrackData>(GetTracksProcedure, parameters);

    }
}
