using Cadenza.Database.SqlLibrary.Model.LastFm;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface ILastFm
{
    Task<List<GetNowPlayingUpdatesResult>> GetNowPlayingUpdates();
    Task MarkNowPlayingUpdated(int userId);
    Task MarkNowPlayingFailed(int userId);

    Task<List<GetNewScrobblesResult>> GetNewScrobbles();
    Task MarkScrobbled(int scrobbleId);
    Task MarkScrobbleFailed(int scrobbleId);
}
