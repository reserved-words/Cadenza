using Cadenza.API.SqlLibrary.Interfaces;
using Dapper;

namespace Cadenza.API.SqlLibrary.Services;

internal class DataDeletionService : IDataDeletionService
{
    private const string DeleteArtistsProcedure = "[Library].[DeleteEmptyArtists]";
    private const string DeleteAlbumsProcedure = "[Library].[DeleteEmptyAlbums]";
    private const string DeleteDiscsProcedure = "[Library].[DeleteEmptyDiscs]";
    private const string DeleteTrackProcedure = "[Library].[DeleteTrack]";
    private const string IdParameter = "Id";
    private const string IdFromSourceParameter = "IdFromSource";

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

    public async Task DeleteTrackById(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, id);
        parameters.Add(IdFromSourceParameter, null);
        await _dbAccess.Execute(DeleteTrackProcedure, parameters);
    }

    public async Task DeleteTrackByIdFromSource(string idFromSource)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, null);
        parameters.Add(IdFromSourceParameter, idFromSource);
        await _dbAccess.Execute(DeleteTrackProcedure, parameters);
    }
}
