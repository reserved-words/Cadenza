using Cadenza.Database.SqlLibrary.Model.Library;
using Cadenza.Database.SqlLibrary.Model.Update;
using Dapper;
using System.Data;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Update : IUpdate
{
    private readonly ISqlAccess _sql;

    public Update(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Update));
    }


    public async Task<int> AddAlbum(AddAlbumParameter data)
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

        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _sql.Execute(parameters);

        return parameters.Get<int>("@Id");
    }

    public async Task<int> AddArtist(AddArtistParameter data)
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

        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _sql.Execute(parameters);

        return parameters.Get<int>("@Id");
    }

    public async Task<int> AddDisc(AddDiscParameter data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.AlbumId), data.AlbumId);
        parameters.Add(nameof(data.Index), data.Index);
        parameters.Add(nameof(data.TrackCount), data.TrackCount);

        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _sql.Execute(parameters);

        return parameters.Get<int>("@Id");
    }

    public async Task<int> AddTrack(AddTrackParameter data)
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

        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _sql.Execute(parameters);

        return parameters.Get<int>("@Id");
    }

    public async Task DeleteEmptyArtists()
    {
        await _sql.Execute(null);
    }

    public async Task DeleteEmptyAlbums()
    {
        await _sql.Execute(null);
    }

    public async Task DeleteEmptyDiscs()
    {
        await _sql.Execute(null);
    }

    public async Task DeleteTrack(int id)
    {
        await _sql.Execute(new { Id = id, IdFromSource = (string)null });
    }

    public async Task DeleteTrack(string idFromSource)
    {
        await _sql.Execute(new { Id = (int?)null, IdFromSource = idFromSource });
    }

    public async Task<GetAlbumForUpdateResult> GetAlbumForUpdate(int id)
    {
        return await _sql.QuerySingle<GetAlbumForUpdateResult>(new { Id = id });
    }

    public async Task<GetArtistForUpdateResult> GetArtistForUpdate(int id)
    {
        return await _sql.QuerySingle<GetArtistForUpdateResult>(new { Id = id });
    }


    public async Task<GetTrackForUpdateResult> GetTrackForUpdate(int id)
    {
        return await _sql.QuerySingle<GetTrackForUpdateResult>(new { Id = id });
    }

    public async Task UpdateAlbum(UpdateAlbumParameter album)
    {
        await _sql.Execute(album);
    }

    public async Task UpdateArtist(UpdateArtistParameter artist)
    {
        await _sql.Execute(artist);
    }

    public async Task UpdateTrack(UpdateTrackParameter track)
    {
        await _sql.Execute(track);
    }
}
