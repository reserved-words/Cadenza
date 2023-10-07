namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record RecentPlayHistoryState(bool IsLoading, List<RecentTrackVM> Tracks) 
{
    private static RecentPlayHistoryState Init() => new RecentPlayHistoryState(true, null);
}
