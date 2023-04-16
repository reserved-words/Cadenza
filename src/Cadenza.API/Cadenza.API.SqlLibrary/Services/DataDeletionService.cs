using Cadenza.API.SqlLibrary.Interfaces;
using Dapper;

namespace Cadenza.API.SqlLibrary.Services;

internal class DataDeletionService : IDataDeletionService
{
    private const string DeleteArtistsProcedure = "[Library].[DeleteEmptyArtists]";
    private const string DeleteAlbumsProcedure = "[Library].[DeleteEmptyAlbums]";
    private const string DeleteDiscsProcedure = "[Library].[DeleteEmptyDiscs]";
    private const string DeleteTrackProcedure = "[Library].[DeleteTrack]";
    private const string IdParameter = "IdFromSource";

    private IDataAccess _dbAccess;

    public DataDeletionService(IDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task DeleteEmptyArtists()
    {
        await _dbAccess.Execute(DeleteArtistsProcedure);
    }

    public async Task DeleteEmptyAlbums()
    {
        await _dbAccess.Execute(DeleteAlbumsProcedure);
    }

    public async Task DeleteEmptyDiscs()
    {
        await _dbAccess.Execute(DeleteDiscsProcedure);
    }

    public async Task DeleteTrack(string id)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, id);
        await _dbAccess.Execute(DeleteTrackProcedure, parameters);
    }
}
