using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;
using Dapper;

namespace Cadenza.API.SqlLibrary.Services;

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
    private const string GetTrackIdsProcedure = "[Library].[GetTrackIds]";

    private const string GetAlbumUpdatesProcedure = "[Queue].[GetAlbumUpdates]";
    private const string GetArtistUpdatesProcedure = "[Queue].[GetArtistUpdates]";
    private const string GetTrackUpdatesProcedure = "[Queue].[GetTrackUpdates]";
    private const string GetTrackRemovalsProcedure = "[Queue].[GetTrackRemovals]";

    private const string GetRecentAlbumsProcedure = "[History].[GetRecentAlbums]";
    private const string GetRecentTagsProcedure = "[History].[GetRecentTags]";

    private const string IdParameter = "@Id";
    private const string IdFromSourceParameter = "@IdFromSource";
    private const string MaxItemsParameter = "@MaxItems";
    private const string NameIdParameter = "@NameId";
    private const string SourceIdParameter = "@SourceId";

    private IDataAccess _dbAccess;

    public DataReadService(IDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
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

    public async Task<List<AlbumUpdateData>> GetAlbumUpdates(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int?)source);
        return await _dbAccess.Query<AlbumUpdateData>(GetAlbumUpdatesProcedure, parameters);
    }

    public async Task<List<string>> GetAllTrackIds(LibrarySource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add(SourceIdParameter, (int)source);
        return await _dbAccess.Query<string>(GetTrackIdsProcedure, parameters);
    }

    public async Task<ArtistData> GetArtist(string nameId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(NameIdParameter, nameId);
        return await _dbAccess.QuerySingle<ArtistData>(GetArtistProcedure, parameters);
    }

    public async Task<ArtistImage> GetArtistImage(string nameId)
    {
        var parameters = new DynamicParameters();
        parameters.Add(NameIdParameter, nameId);
        return await _dbAccess.QuerySingle<ArtistImage>(GetArtistImageProcedure, parameters);
    }

    public async Task<List<GetArtistData>> GetArtists()
    {
        return await _dbAccess.Query<GetArtistData>(GetArtistsProcedure, null);
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

    public async Task<TrackData> GetTrack(string idFromSource)
    {
        var parameters = new DynamicParameters();
        parameters.Add(IdFromSourceParameter, idFromSource);
        return await _dbAccess.QuerySingle<TrackData>(GetTrackProcedure, parameters);
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
