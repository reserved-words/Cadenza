using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IHistory
{
    Task<List<GetRecentAlbumsResult>> GetRecentAlbums(int maxItems);
    Task<List<GetRecentTagsResult>> GetRecentTags(int maxItems);

    Task<List<GetNowPlayingUpdatesResult>> GetNowPlayingUpdates();
    Task InsertNowPlaying(InsertNowPlayingParameter parameters);
    Task MarkNowPlayingUpdated(int userId);
    Task MarkNowPlayingFailed(int userId);

    Task<List<GetNewScrobblesResult>> GetNewScrobbles();
    Task InsertScrobble(InsertScrobbleParameter parameters);
    Task MarkScrobbled(int scrobbleId);
    Task MarkScrobbleFailed(int scrobbleId);
}
