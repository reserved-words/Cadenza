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

        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _sql.Execute(parameters);

        return parameters.Get<int>("@Id");
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

        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _sql.Execute(parameters);

        return parameters.Get<int>("@Id");
    }

    public async Task<int> AddDisc(NewDiscData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.AlbumId), data.AlbumId);
        parameters.Add(nameof(data.Index), data.Index);
        parameters.Add(nameof(data.TrackCount), data.TrackCount);

        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _sql.Execute(parameters);

        return parameters.Get<int>("@Id");
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
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);
        parameters.Add("IdFromSource", null);
        await _sql.Execute(parameters);
    }

    public async Task DeleteTrack(string idFromSource)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", null);
        parameters.Add("IdFromSource", idFromSource);
        await _sql.Execute(parameters);
    }

    public async Task UpdateAlbum(AlbumData album)
    {
        await _sql.Execute(album);
    }

    public async Task UpdateArtist(ArtistData artist)
    {
        await _sql.Execute(artist);
    }

    public async Task UpdateTrack(TrackData track)
    {
        await _sql.Execute(track);
    }
}
