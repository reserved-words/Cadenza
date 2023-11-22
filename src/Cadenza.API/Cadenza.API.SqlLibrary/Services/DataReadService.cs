using Dapper;

namespace Cadenza.Database.SqlLibrary.Services;

internal class DataReadService : IDataReadService
{
    private const string GetAlbumProcedure = "[Library].[GetAlbum]";
    private const string GetAlbumArtworkProcedure = "[Library].[GetAlbumArtwork]";
    private const string GetAlbumsProcedure = "[Library].[GetAlbums]";
    private const string GetArtistProcedure = "[Library].[GetArtist]";
    private const string GetArtistImageProcedure = "[Library].[GetArtistImage]";
    private const string GetArtistsProcedure = "[Library].[GetArtists]";
    private const string GetDiscsProcedure = "[Library].[GetDiscs]";
    private const string GetTrackProcedure = "[Library].[GetTrack]";
    private const string GetTracksProcedure = "[Library].[GetTracks]";
    private const string GetTrackSourceIdsProcedure = "[Library].[GetTrackSourceIds]";

    private const string GetTrackIdFromSourceProcedure = "[Library].[GetTrackIdFromSource]";
    private const string GetAlbumTrackSourceIdsProcedure = "[Library].[GetAlbumTrackSourceIds]";
    private const string GetArtistTrackSourceIdsProcedure = "[Library].[GetArtistTrackSourceIds]";

    private const string GetAlbumUpdatesProcedure = "[Queue].[GetAlbumUpdates]";
    private const string GetArtistUpdatesProcedure = "[Queue].[GetArtistUpdates]";
    private const string GetTrackUpdatesProcedure = "[Queue].[GetTrackUpdates]";
    private const string GetTrackRemovalsProcedure = "[Queue].[GetTrackRemovals]";

    private const string GetRecentAlbumsProcedure = "[History].[GetRecentAlbums]";
    private const string GetRecentTagsProcedure = "[History].[GetRecentTags]";

    private const string GetGroupingsProcedure = "[Admin].[GetGroupings]";

    private const string GetAllTrackIdsProcedure = "[Play].[GetAllTrackIds]";
    private const string GetAlbumTrackIdsProcedure = "[Play].[GetAlbumTrackIds]";
    private const string GetArtistTrackIdsProcedure = "[Play].[GetArtistTrackIds]";
    private const string GetGenreTrackIdsProcedure = "[Play].[GetGenreTrackIds]";
    private const string GetGroupingTrackIdsProcedure = "[Play].[GetGroupingTrackIds]";
    private const string GetTagTrackIdsProcedure = "[Play].[GetTagTrackIds]";

    private const string IdParameter = "@Id";
    private const string GenreParameter = "@Genre";
    private const string MaxItemsParameter = "@MaxItems";
    private const string SourceIdParameter = "@SourceId";
    private const string TagParameter = "@Tag";

    private IDataAccess _dbAccess;

    public DataReadService(IDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<List<int>> GetAbumTrackIds(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, albumId);
        return await _dbAccess.Query<int>(GetAlbumTrackIdsProcedure, parameters);
    }

    public async Task<AlbumData> GetAlbum(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, albumId);
        return await _dbAccess.QuerySingle<AlbumData>(GetAlbumProcedure, parameters);
    }

    public async Task<AlbumArtwork> GetAlbumArtwork(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, albumId);
        return await _dbAccess.QuerySingle<AlbumArtwork>(GetAlbumArtworkProcedure, parameters);
    }

    public async Task<List<GetAlbumData>> GetAlbums(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<GetAlbumData>(GetAlbumsProcedure, parameters);
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, albumId);
        return await _dbAccess.Query<string>(GetAlbumTrackSourceIdsProcedure, parameters);
    }

    public async Task<List<AlbumUpdateData>> GetAlbumUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<AlbumUpdateData>(GetAlbumUpdatesProcedure, parameters);
    }

    public async Task<List<int>> GetAllTrackIds()
    {
        return await _dbAccess.Query<int>(GetAllTrackIdsProcedure);
    }

    public async Task<List<string>> GetAllTrackSourceIds(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int)source);
        return await _dbAccess.Query<string>(GetTrackSourceIdsProcedure, parameters);
    }

    public async Task<ArtistData> GetArtist(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, artistId);
        return await _dbAccess.QuerySingle<ArtistData>(GetArtistProcedure, parameters);
    }

    public async Task<ArtistImage> GetArtistImage(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, artistId);
        return await _dbAccess.QuerySingle<ArtistImage>(GetArtistImageProcedure, parameters);
    }

    public async Task<List<GetArtistData>> GetArtists()
    {
        return await _dbAccess.Query<GetArtistData>(GetArtistsProcedure, null);
    }

    public async Task<List<int>> GetArtistTrackIds(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, artistId);
        return await _dbAccess.Query<int>(GetArtistTrackIdsProcedure, parameters);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, artistId);
        return await _dbAccess.Query<string>(GetArtistTrackSourceIdsProcedure, parameters);
    }

    public async Task<List<ArtistUpdateData>> GetArtistUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<ArtistUpdateData>(GetArtistUpdatesProcedure, parameters);
    }

    public async Task<List<GetDiscData>> GetDiscs(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<GetDiscData>(GetDiscsProcedure, parameters);
    }

    public async Task<List<int>> GetGenreTrackIds(string genre)
    {
        var parameters = new DynamicParameters();
        parameters.Add(GenreParameter, genre);
        return await _dbAccess.Query<int>(GetGenreTrackIdsProcedure, parameters);
    }

    public async Task<List<GroupingDTO>> GetGroupings()
    {
        // should be using Data model not DTO here
        return await _dbAccess.Query<GroupingDTO>(GetGroupingsProcedure);
    }

    public async Task<List<int>> GetGroupingTrackIds(int groupingId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, groupingId);
        return await _dbAccess.Query<int>(GetGroupingTrackIdsProcedure, parameters);
    }

    public async Task<List<RecentAlbumData>> GetRecentAlbums(int maxItems)
    {
        var parameters = new DynamicParameters();
        parameters.Add(MaxItemsParameter, maxItems);
        return await _dbAccess.Query<RecentAlbumData>(GetRecentAlbumsProcedure, parameters);
    }

    public async Task<List<RecentTagData>> GetRecentTags(int maxItems)
    {
        var parameters = new DynamicParameters();
        parameters.Add(MaxItemsParameter, maxItems);
        return await _dbAccess.Query<RecentTagData>(GetRecentTagsProcedure, parameters);
    }

    public async Task<List<int>> GetTagTrackIds(string tag)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TagParameter, tag);
        return await _dbAccess.Query<int>(GetTagTrackIdsProcedure, parameters);
    }

    public async Task<TrackData> GetTrack(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, trackId);
        return await _dbAccess.QuerySingle<TrackData>(GetTrackProcedure, parameters);
    }

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdParameter, trackId);
        return await _dbAccess.QuerySingle<string>(GetTrackIdFromSourceProcedure, parameters);
    }

    public async Task<List<TrackRemovalData>> GetTrackRemovals(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<TrackRemovalData>(GetTrackRemovalsProcedure, parameters);
    }

    public async Task<List<GetTrackData>> GetTracks(LibrarySource? source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<GetTrackData>(GetTracksProcedure, parameters);

    }

    public async Task<List<TrackUpdateData>> GetTrackUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<TrackUpdateData>(GetTrackUpdatesProcedure, parameters);
    }
}
