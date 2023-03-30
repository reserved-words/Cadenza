using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;
using Dapper;

namespace Cadenza.API.SqlLibrary.Services;

internal class DataUpdateService : IDataUpdateService
{
    private const string UpdateAlbumProcedure = "[Library].[UpdateAlbum]";
    private const string UpdateArtistProcedure = "[Library].[UpdateArtist]";
    private const string UpdateTrackProcedure = "[Library].[UpdateTrack]";
    private const string MarkAlbumUpdateDoneProcedure = "[Queue].[MarkAlbumUpdateDone]";
    private const string MarkArtistUpdateDoneProcedure = "[Queue].[MarkArtistUpdateDone]";
    private const string MarkTrackUpdateDoneProcedure = "[Queue].[MarkTrackUpdateDone]";

    private const string IdParameter = "@Id";

    private IDataAccess _dbAccess;

    public DataUpdateService(IDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task MarkAlbumUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, id);
        await _dbAccess.Execute(MarkAlbumUpdateDoneProcedure, parameters);
    }

    public async Task MarkArtistUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, id);
        await _dbAccess.Execute(MarkArtistUpdateDoneProcedure, parameters);
    }

    public async Task MarkTrackUpdateDone(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, id);
        await _dbAccess.Execute(MarkTrackUpdateDoneProcedure, parameters);
    }

    public async Task UpdateAlbum(AlbumData album)
    {
        await _dbAccess.Execute(UpdateAlbumProcedure, album);
    }

    public async Task UpdateArtist(ArtistData artist)
    {
        await _dbAccess.Execute(UpdateArtistProcedure, artist);
    }

    public async Task UpdateTrack(TrackData track)
    {
        await _dbAccess.Execute(UpdateTrackProcedure, track);
    }
}
