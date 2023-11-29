using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IHistory
{
    Task<List<GetTopAlbumsResult>> GetTopAlbums(GetTopAlbumsParameter parameter);
    Task<List<GetTopArtistsResult>> GetTopArtists(GetTopArtistsParameter parameter);
    Task<List<GetTopTracksResult>> GetTopTracks(GetTopTracksParameter parameter);

    Task<List<GetRecentAlbumsResult>> GetRecentAlbums(int maxItems);
    Task<List<GetRecentTagsResult>> GetRecentTags(int maxItems);

    Task InsertNowPlaying(InsertNowPlayingParameter parameters);
    Task InsertScrobble(InsertScrobbleParameter parameters);
    Task<IEnumerable<GetRecentTracksResult>> GetRecentTracks(string username, int maxItems);

    Task SyncScrobbles();
}
