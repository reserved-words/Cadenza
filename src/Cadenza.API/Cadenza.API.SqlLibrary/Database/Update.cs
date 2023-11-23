using Dapper;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Update : IUpdate
{
    private ISqlAccess _sql;

    public Update(ISqlAccess sql)
    {
        _sql = sql;
    }

    public async Task DeleteEmptyArtists()
    {
        await _sql.Execute("[Library].[DeleteEmptyArtists]");
    }

    public async Task DeleteEmptyAlbums()
    {
        await _sql.Execute("[Library].[DeleteEmptyAlbums]");
    }

    public async Task DeleteEmptyDiscs()
    {
        await _sql.Execute("[Library].[DeleteEmptyDiscs]");
    }

    public async Task DeleteTrackById(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);
        parameters.Add("IdFromSource", null);
        await _sql.Execute("[Library].[DeleteTrack]", parameters);
    }

    public async Task DeleteTrackByIdFromSource(string idFromSource)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", null);
        parameters.Add("IdFromSource", idFromSource);
        await _sql.Execute("[Library].[DeleteTrack]", parameters);
    }

    public async Task UpdateAlbum(AlbumData album)
    {
        await _sql.Execute("[Library].[UpdateAlbum]", album);
    }

    public async Task UpdateArtist(ArtistData artist)
    {
        await _sql.Execute("[Library].[UpdateArtist]", artist);
    }

    public async Task UpdateTrack(TrackData track)
    {
        await _sql.Execute("[Library].[UpdateTrack]", track);
    }
}
