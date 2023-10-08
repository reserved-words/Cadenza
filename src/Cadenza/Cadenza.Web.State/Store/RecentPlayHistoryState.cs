namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record RecentPlayHistoryState(bool IsLoading, IReadOnlyCollection<RecentTrackVM> Tracks)
{
    private static RecentPlayHistoryState Init() => new RecentPlayHistoryState(true, null);
}
