using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record HistoryRecentlyPlayedTracksState(bool IsLoading, IReadOnlyCollection<RecentTrackVM> Tracks)
{
    private static HistoryRecentlyPlayedTracksState Init() => new HistoryRecentlyPlayedTracksState(true, null);
}
