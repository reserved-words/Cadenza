using Cadenza.Database.SqlLibrary.Model.LastFm;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface ILastFm
{
    Task<List<GetLovedTrackUpdatesResult>> GetLovedTrackUpdates();
    Task MarkLovedTrackUpdated(int trackId);
    Task MarkLovedTrackFailed(int trackId);

    Task<List<GetNowPlayingUpdatesResult>> GetNowPlayingUpdates();
    Task MarkNowPlayingUpdated();
    Task MarkNowPlayingFailed();

    Task<List<GetNewScrobblesResult>> GetNewScrobbles();
    Task MarkScrobbled(int scrobbleId);
    Task MarkScrobbleFailed(int scrobbleId);
}
