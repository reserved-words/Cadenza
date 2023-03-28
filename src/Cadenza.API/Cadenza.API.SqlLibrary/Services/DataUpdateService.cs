using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Services;

internal class DataUpdateService : IDataUpdateService
{
    private const string UpdateAlbumProcedure = "[Library].[UpdateAlbum]";
    private const string UpdateArtistProcedure = "[Library].[UpdateArtist]";
    private const string UpdateTrackProcedure = "[Library].[UpdateTrack]";

    private IDataAccess _dbAccess;

    public DataUpdateService(IDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
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
