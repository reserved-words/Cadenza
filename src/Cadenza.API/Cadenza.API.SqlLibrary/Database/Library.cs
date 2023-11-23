using Dapper;
using System.Data;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Library : ILibrary
{
    private ISqlAccess _sql;

    public Library(ISqlAccess sql)
    {
        _sql = sql;
    }public async Task<AlbumData> GetAlbum(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.QuerySingle<AlbumData>("[Library].[GetAlbum]", parameters);
    }

    public async Task<AlbumArtwork> GetAlbumArtwork(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.QuerySingle<AlbumArtwork>("[Library].[GetAlbumArtwork]", parameters);
    }

    public async Task<List<GetAlbumData>> GetAlbums(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetAlbumData>("[Library].[GetAlbums]", parameters);
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", albumId);
        return await _sql.Query<string>("[Library].[GetAlbumTrackSourceIds]", parameters);
    }

    public async Task<List<string>> GetAllTrackSourceIds(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int)source);
        return await _sql.Query<string>("[Library].[GetTrackSourceIds]", parameters);
    }

    public async Task<ArtistData> GetArtist(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.QuerySingle<ArtistData>("[Library].[GetArtist]", parameters);
    }

    public async Task<ArtistImage> GetArtistImage(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.QuerySingle<ArtistImage>("[Library].[GetArtistImage]", parameters);
    }

    public async Task<List<GetArtistData>> GetArtists()
    {
        return await _sql.Query<GetArtistData>("[Library].[GetArtists]", null);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", artistId);
        return await _sql.Query<string>("[Library].[GetArtistTrackSourceIds]", parameters);
    }

    public async Task<List<GetDiscData>> GetDiscs(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetDiscData>("[Library].[GetDiscs]", parameters);
    }


    public async Task<TrackData> GetTrack(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", trackId);
        return await _sql.QuerySingle<TrackData>("[Library].[GetTrack]", parameters);
    }

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", trackId);
        return await _sql.QuerySingle<string>("[Library].[GetTrackIdFromSource]", parameters);
    }

    public async Task<List<GetTrackData>> GetTracks(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@SourceId", (int?)source);
        return await _sql.Query<GetTrackData>("[Library].[GetTracks]", parameters);

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

        await _sql.Execute("[Library].[AddAlbum]", parameters);

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

        await _sql.Execute("[Library].[AddArtist]", parameters);

        return parameters.Get<int>("@Id");
    }

    public async Task<int> AddDisc(NewDiscData data)
    {
        var parameters = new DynamicParameters();

        parameters.Add(nameof(data.AlbumId), data.AlbumId);
        parameters.Add(nameof(data.Index), data.Index);
        parameters.Add(nameof(data.TrackCount), data.TrackCount);

        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _sql.Execute("[Library].[AddDisc]", parameters);

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

        await _sql.Execute("[Library].[AddTrack]", parameters);

        return parameters.Get<int>("@Id");
    }
}
