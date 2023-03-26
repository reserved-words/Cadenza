using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;
using Dapper;
using System.Data;

namespace Cadenza.API.SqlLibrary.Services;
internal class InsertService : IInsertService
{
    private const string AddArtistProcedure = "[Library].[AddArtist]";
    private const string AddAlbumProcedure = "[Library].[AddAlbum]";
    private const string AddDiscProcedure = "[Library].[AddDisc]";
    private const string AddTrackProcedure = "[Library].[AddTrack]";
    private const string IdParameter = "Id";

    private IDatabaseAccess _dbAccess;

    public InsertService(IDatabaseAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<int> AddAlbum(AddAlbumData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.SourceId), data.SourceId);
        parameters.Add(nameof(data.ArtistId), data.ArtistId);
        parameters.Add(nameof(data.Title), data.Title);
        parameters.Add(nameof(data.ReleaseTypeId), data.ReleaseTypeId);
        parameters.Add(nameof(data.Year), data.Year);
        parameters.Add(nameof(data.ArtworkUrl), data.ArtworkUrl);
        parameters.Add(nameof(data.TagList), data.TagList);

        parameters.Add(IdParameter, dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _dbAccess.Execute(AddAlbumProcedure, parameters);

        return parameters.Get<int>(IdParameter);
    }

    public async Task<int> AddArtist(AddArtistData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.NameId), data.NameId);
        parameters.Add(nameof(data.Name), data.Name);
        parameters.Add(nameof(data.GroupingId), data.GroupingId);
        parameters.Add(nameof(data.Genre), data.Genre);
        parameters.Add(nameof(data.City), data.City);
        parameters.Add(nameof(data.State), data.State);
        parameters.Add(nameof(data.Country), data.Country);
        parameters.Add(nameof(data.ImageUrl), data.ImageUrl);
        parameters.Add(nameof(data.TagList), data.TagList);

        parameters.Add(IdParameter, dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _dbAccess.Execute(AddArtistProcedure, parameters);

        return parameters.Get<int>(IdParameter);
    }

    public async Task<int> AddDisc(AddDiscData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.AlbumId), data.AlbumId);
        parameters.Add(nameof(data.Index), data.Index);
        parameters.Add(nameof(data.TrackCount), data.TrackCount);

        parameters.Add(IdParameter, dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _dbAccess.Execute(AddDiscProcedure, parameters);

        return parameters.Get<int>(IdParameter);
    }

    public async Task<int> AddTrack(AddTrackData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.IdFromSource), data.IdFromSource);
        parameters.Add(nameof(data.ArtistId), data.ArtistId);
        parameters.Add(nameof(data.DiscId), data.DiscId);
        parameters.Add(nameof(data.TrackNo), data.TrackNo);
        parameters.Add(nameof(data.Title), data.Title);
        parameters.Add(nameof(data.DurationSeconds), data.DurationSeconds);
        parameters.Add(nameof(data.Year), data.Year);
        parameters.Add(nameof(data.Lyrics), data.Lyrics);
        parameters.Add(nameof(data.TagList), data.TagList);

        parameters.Add(IdParameter, dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _dbAccess.Execute(AddTrackProcedure, parameters);

        return parameters.Get<int>(IdParameter);
    }
}
