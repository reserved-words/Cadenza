using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;
using Dapper;
using System.Data;

namespace Cadenza.API.SqlLibrary.Services;

internal class DataInsertService : IDataInsertService
{
    private const string AddArtistProcedure = "[Library].[AddArtist]";
    private const string AddAlbumProcedure = "[Library].[AddAlbum]";
    private const string AddDiscProcedure = "[Library].[AddDisc]";
    private const string AddTrackProcedure = "[Library].[AddTrack]";
    
    private const string AddAlbumUpdateProcedure = "[Queue].[AddAlbumUpdate]";
    private const string AddArtistUpdateProcedure = "[Queue].[AddArtistUpdate]";
    private const string AddTrackUpdateProcedure = "[Queue].[AddTrackUpdate]";
    private const string AddTrackRemovalProcedure = "[Queue].[AddTrackRemoval]";

    private const string LogLibraryPlayProcedure = "[History].[LogLibraryPlay]";
    private const string LogArtistPlayProcedure = "[History].[LogArtistPlay]";
    private const string LogAlbumPlayProcedure = "[History].[LogAlbumPlay]";
    private const string LogTrackPlayProcedure = "[History].[LogTrackPlay]";
    private const string LogGroupingPlayProcedure = "[History].[LogGroupingPlay]";
    private const string LogGenrePlayProcedure = "[History].[LogGenrePlay]";
    private const string LogTagPlayProcedure = "[History].[LogTagPlay]";

    private const string AlbumIdParameter = "AlbumId";
    private const string ArtistIdParameter = "ArtistId";
    private const string IdParameter = "Id";
    private const string GenreParameter = "Genre";
    private const string GroupingIdParameter = "GroupingId";
    private const string TagParameter = "Tag";
    private const string TrackIdParameter = "TrackId";

    private IDataAccess _dbAccess;

    public DataInsertService(IDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<int> AddAlbum(NewAlbumData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.SourceId), data.SourceId);
        parameters.Add(nameof(data.ArtistId), data.ArtistId);
        parameters.Add(nameof(data.Title), data.Title);
        parameters.Add(nameof(data.ReleaseTypeId), data.ReleaseTypeId);
        parameters.Add(nameof(data.Year), data.Year);
        parameters.Add(nameof(data.DiscCount), data.DiscCount);
        parameters.Add(nameof(data.ArtworkMimeType), data.ArtworkMimeType);
        parameters.Add(nameof(data.ArtworkContent), data.ArtworkContent, DbType.Binary);
        parameters.Add(nameof(data.TagList), data.TagList);

        parameters.Add(IdParameter, dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _dbAccess.Execute(AddAlbumProcedure, parameters);

        return parameters.Get<int>(IdParameter);
    }

    public async Task AddAlbumUpdate(NewAlbumUpdateData data)
    {
        await _dbAccess.Execute(AddAlbumUpdateProcedure, data);
    }

    public async Task<int> AddArtist(NewArtistData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.Name), data.Name);
        parameters.Add(nameof(data.CompareName), data.CompareName);
        parameters.Add(nameof(data.GroupingName), data.GroupingName);
        parameters.Add(nameof(data.Genre), data.Genre);
        parameters.Add(nameof(data.City), data.City);
        parameters.Add(nameof(data.State), data.State);
        parameters.Add(nameof(data.Country), data.Country);
        parameters.Add(nameof(data.ImageMimeType), data.ImageMimeType);
        parameters.Add(nameof(data.ImageContent), data.ImageContent, DbType.Binary);
        parameters.Add(nameof(data.TagList), data.TagList);

        parameters.Add(IdParameter, dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _dbAccess.Execute(AddArtistProcedure, parameters);

        return parameters.Get<int>(IdParameter);
    }

    public async Task AddArtistUpdate(NewArtistUpdateData data)
    {
        await _dbAccess.Execute(AddArtistUpdateProcedure, data);
    }

    public async Task<int> AddDisc(NewDiscData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.AlbumId), data.AlbumId);
        parameters.Add(nameof(data.Index), data.Index);
        parameters.Add(nameof(data.TrackCount), data.TrackCount);

        parameters.Add(IdParameter, dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _dbAccess.Execute(AddDiscProcedure, parameters);

        return parameters.Get<int>(IdParameter);
    }

    public async Task<int> AddTrack(NewTrackData data)
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

    public async Task AddTrackUpdate(NewTrackUpdateData data)
    {
        await _dbAccess.Execute(AddTrackUpdateProcedure, data);
    }

    public async Task AddTrackRemoval(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, trackId);
        await _dbAccess.Execute(AddTrackRemovalProcedure, parameters);
    }

    public async Task LogAlbumPlay(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(AlbumIdParameter, albumId);
        await _dbAccess.Execute(LogAlbumPlayProcedure, parameters);
    }

    public async Task LogArtistPlay(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(ArtistIdParameter, artistId);
        await _dbAccess.Execute(LogArtistPlayProcedure, parameters);
    }

    public async Task LogGenrePlay(string genre)
    {
        var parameters = new DynamicParameters();
        parameters.Add(GenreParameter, genre);
        await _dbAccess.Execute(LogGenrePlayProcedure, parameters);
    }

    public async Task LogGroupingPlay(int groupingId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(GroupingIdParameter, groupingId);
        await _dbAccess.Execute(LogGroupingPlayProcedure, parameters);
    }

    public async Task LogLibraryPlay()
    {
        await _dbAccess.Execute(LogLibraryPlayProcedure);
    }

    public async Task LogTagPlay(string tag)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TagParameter, tag);
        await _dbAccess.Execute(LogTagPlayProcedure, parameters);
    }

    public async Task LogTrackPlay(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TrackIdParameter, trackId);
        await _dbAccess.Execute(LogTrackPlayProcedure, parameters);
    }
}
